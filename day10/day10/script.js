const KEY_USERS='app_users',KEY_EVENTS='events',KEY_GUESTS='guests',KEY_CATEGORIES='categories',KEY_BACKUPS='backups',KEY_CURRENT='current_user';

/* ---------- STORAGE HELPERS ---------- */
function getUsers(){return JSON.parse(localStorage.getItem(KEY_USERS)||'[]');}
function setUsers(users){localStorage.setItem(KEY_USERS,JSON.stringify(users));}
function getEvents(){return JSON.parse(localStorage.getItem(KEY_EVENTS)||'[]');}
function setEvents(events){localStorage.setItem(KEY_EVENTS,JSON.stringify(events));}
function getGuests(){return JSON.parse(localStorage.getItem(KEY_GUESTS)||'[]');}
function setGuests(guests){localStorage.setItem(KEY_GUESTS,JSON.stringify(guests));}
function getCategories(){return JSON.parse(localStorage.getItem(KEY_CATEGORIES)||'[]');}
function setCategories(categories){localStorage.setItem(KEY_CATEGORIES,JSON.stringify(categories));}
function getBackups(){return JSON.parse(localStorage.getItem(KEY_BACKUPS)||'[]');}
function setBackups(backups){localStorage.setItem(KEY_BACKUPS,JSON.stringify(backups));}
function getCurrentUser(){return JSON.parse(localStorage.getItem(KEY_CURRENT));}
function setCurrentUser(user){localStorage.setItem(KEY_CURRENT,JSON.stringify(user));}
function clearLocal(){localStorage.clear(); location.reload();}

/* ---------- LOGIN ---------- */
function loginNoCredential(){
  const name=document.getElementById('loginUser').value.trim();
  if(!name){alert('Enter name'); return;}
  const role=document.getElementById('loginRole').value;
  const user={name,role};
  setCurrentUser(user);
  document.getElementById('auth-area').style.display='none';
  document.querySelector('.app').style.display='flex';
  initApp();
}

/* ---------- LOGOUT ---------- */
function logout(){localStorage.removeItem(KEY_CURRENT);location.reload();}

/* ---------- APP INIT ---------- */
function initApp(){
  const user=getCurrentUser();
  if(!user) return;
  document.getElementById('welcomeText').textContent=`Hello, ${user.name}`;
  document.getElementById('roleLabel').textContent=user.role;
  document.querySelectorAll('.nav-admin,.nav-user').forEach(el=>el.style.display='none');
  if(user.role==='Admin') document.querySelector('.nav-admin').style.display='flex';
  else document.querySelector('.nav-user').style.display='flex';
  if(user.role==='Admin') showAdminSection('dashboard'); else showUserSection('events');
  renderAll();
}

// ---------- ADMIN DASHBOARD FUNCTIONS ----------

// Update dashboard tiles counts
function updateAdminDashboard() {
  const users = JSON.parse(localStorage.getItem('app_users') || '[]');
  const categories = JSON.parse(localStorage.getItem('categories') || '[]');
  const events = JSON.parse(localStorage.getItem('events') || '[]');
  const backups = JSON.parse(localStorage.getItem('backups') || '[]');

  document.getElementById('adminUserCount').textContent = users.length;
  document.getElementById('adminCategoryCount').textContent = categories.length;
  document.getElementById('adminEventCount').textContent = events.length;
  document.getElementById('adminBackupCount').textContent = backups.length;
}

// ---------------- QUICK ACTIONS ----------------

// Create a new Admin user
function openCreateAdmin() {
  const name = prompt("Enter new admin name:");
  if(!name) return;

  let users = JSON.parse(localStorage.getItem('app_users') || '[]');
  // Check if username already exists
  if(users.some(u => u.name === name)) {
    alert("Username already exists!");
    return;
  }

  users.push({name: name.trim(), email: '', role: 'Admin'});
  localStorage.setItem('app_users', JSON.stringify(users));
  alert("Admin created successfully!");
  updateAdminDashboard();
}

// Export all data as JSON
function downloadData() {
  const data = {
    users: JSON.parse(localStorage.getItem('app_users') || '[]'),
    categories: JSON.parse(localStorage.getItem('categories') || '[]'),
    events: JSON.parse(localStorage.getItem('events') || '[]'),
    guests: JSON.parse(localStorage.getItem('guests') || '[]'),
    backups: JSON.parse(localStorage.getItem('backups') || '[]')
  };

  const blob = new Blob([JSON.stringify(data, null, 2)], { type: 'application/json' });
  const url = URL.createObjectURL(blob);
  const a = document.createElement('a');
  a.href = url;
  a.download = 'event_planner_data.json';
  a.click();
  URL.revokeObjectURL(url);
}

// Clear all stored data
function clearAllData() {
  if(confirm("Are you sure you want to clear all stored data?")) {
    localStorage.clear();
    alert("All data cleared!");
    location.reload();
  }
}

// ---------------- INITIALIZATION ----------------
window.addEventListener('load', () => {
  updateAdminDashboard();
});

/* ---------- NAVIGATION ---------- */
function showAdminSection(id){document.querySelectorAll('.section').forEach(s=>s.style.display='none');document.getElementById(`admin-${id}`).style.display='block';document.getElementById('pageTitle').textContent=id.charAt(0).toUpperCase()+id.slice(1);}
function showUserSection(id){document.querySelectorAll('.section').forEach(s=>s.style.display='none');document.getElementById(`user-${id}`).style.display='block';document.getElementById('pageTitle').textContent=id.charAt(0).toUpperCase()+id.slice(1);}

/* ---------- CATEGORIES ---------- */


// Add new category
function addCategory() {
  const val = document.getElementById('newCategory').value.trim();
  if (!val) { alert('Enter category'); return; }

  const cats = getCategories();
  if (cats.includes(val)) { alert('Category already exists'); return; }

  cats.push(val);
  setCategories(cats);
  document.getElementById('newCategory').value = '';
  renderCategories();
  renderAdminDashboard();
}

// Render categories table with Edit/Delete
function renderCategories() {
  const cats = getCategories();
  const tbody = document.getElementById('categoryTable');
  tbody.innerHTML = '';

  cats.forEach((c, i) => {
    const tr = document.createElement('tr');
    tr.innerHTML = `
      <td>${c}</td>
      <td>
        <button class="small blue" onclick="editCategory(${i})">Edit</button>
        <button class="small danger" onclick="deleteCategory(${i})">Delete</button>
      </td>
    `;
    tbody.appendChild(tr);
  });
}

// Edit category
function editCategory(index) {
  const cats = getCategories();
  const newName = prompt("Edit category name:", cats[index]);
  if (!newName) return;

  if (cats.includes(newName.trim())) { 
    alert('Category already exists'); 
    return; 
  }

  cats[index] = newName.trim();
  setCategories(cats);
  renderCategories();
  renderAdminDashboard();
}

// Delete category
function deleteCategory(index) {
  if (!confirm("Are you sure you want to delete this category?")) return;

  const cats = getCategories();
  cats.splice(index, 1);
  setCategories(cats);
  renderCategories();
  renderAdminDashboard();
}


/* ---------- EVENTS ---------- */
function openEventModal(){document.getElementById('eventModal').classList.add('show'); populateEventCategorySelect();}
function closeEventModal(){document.getElementById('eventModal').classList.remove('show');document.getElementById('eventModal').dataset.editIndex=undefined; document.getElementById('evName').value='';document.getElementById('evDate').value='';document.getElementById('evTime').value='';document.getElementById('evDesc').value='';}
function populateEventCategorySelect(){const sel=document.getElementById('evCategory');sel.innerHTML='';getCategories().forEach(c=>{sel.innerHTML+=`<option value="${c}">${c}</option>`});}
function saveEvent(){
  const name=document.getElementById('evName').value.trim();
  if(!name){alert('Enter name'); return;}
  const events=getEvents();
  const event={name,date:document.getElementById('evDate').value,time:document.getElementById('evTime').value,category:document.getElementById('evCategory').value,status:document.getElementById('evStatus').value,desc:document.getElementById('evDesc').value};
  const editIndex=document.getElementById('eventModal').dataset.editIndex;
  if(editIndex!==undefined) events[editIndex]=event; else events.push(event);
  setEvents(events);closeEventModal();renderEvents();populateEventSelect();populateAgendaEventSelect();renderAll();
}
function renderEvents(){
  const list=document.getElementById('eventsList');if(!list) return;
  const filter=document.getElementById('searchEvent')?.value.toLowerCase()||'';
  const statusFilter=document.getElementById('filterEventStatus')?.value||'';
  list.innerHTML='';getEvents().filter(e=>e.name.toLowerCase().includes(filter)&&(statusFilter?e.status===statusFilter:true)).forEach((e,i)=>{
    const div=document.createElement('div');div.className='card';
    div.innerHTML=`<strong>${e.name}</strong> | ${e.date} ${e.time} | ${e.category} | ${e.status}<br>${e.desc} <button class="small blue" onclick="editEvent(${i})">Edit</button> <button class="small danger" onclick="deleteEvent(${i})">Delete</button>`;
    list.appendChild(div);
  });
}
function editEvent(i){const e=getEvents()[i];document.getElementById('evName').value=e.name;document.getElementById('evDate').value=e.date;document.getElementById('evTime').value=e.time;document.getElementById('evCategory').value=e.category;document.getElementById('evStatus').value=e.status;document.getElementById('evDesc').value=e.desc;document.getElementById('eventModal').dataset.editIndex=i;openEventModal();}
function deleteEvent(i){const events=getEvents();events.splice(i,1);setEvents(events);renderEvents();populateEventSelect();populateAgendaEventSelect();renderAll();}

/* ---------- GUESTS ---------- */
function populateEventSelect(id='guestEventSelect'){const sel=document.getElementById(id);if(!sel) return;const events=getEvents();sel.innerHTML='<option value="">Select event</option>'+events.map(e=>`<option value="${e.name}">${e.name} (${e.date})</option>`).join('');}
function populateAgendaEventSelect(){populateEventSelect('agendaEventSelect');}
function addOrUpdateGuest(){
  const name=document.getElementById('guestName').value.trim();
  const email=document.getElementById('guestEmail').value.trim();
  const event=document.getElementById('guestEventSelect').value;
  const rsvp=document.getElementById('guestRSVP').value.trim();
  if(!name||!email||!event){alert('Fill all');return;}
  const guests=getGuests();
  const existing=guests.findIndex(g=>g.email===email && g.event===event);
  if(existing!==-1) guests[existing]={name,email,event,rsvp};
  else guests.push({name,email,event,rsvp});
  setGuests(guests);renderGuests();document.getElementById('guestName').value='';document.getElementById('guestEmail').value='';document.getElementById('guestRSVP').value='';document.getElementById('guestEventSelect').value='';}
function renderGuests(){
  const tbody=document.querySelector('#guestTable tbody');tbody.innerHTML='';getGuests().forEach((g,i)=>{const tr=document.createElement('tr');tr.innerHTML=`<td>${g.name}</td><td>${g.email}</td><td>${g.event}</td><td>${g.rsvp}</td><td><button class="small blue" onclick="editGuest(${i})">Edit</button> <button class="small danger" onclick="deleteGuest(${i})">Delete</button></td>`;tbody.appendChild(tr);});}
function editGuest(i){const g=getGuests()[i];document.getElementById('guestName').value=g.name;document.getElementById('guestEmail').value=g.email;document.getElementById('guestRSVP').value=g.rsvp;document.getElementById('guestEventSelect').value=g.event;}
function deleteGuest(i){const guests=getGuests();guests.splice(i,1);setGuests(guests);renderGuests();}
function sendInvitations(){alert('Invitations sent!');}

/* ---------- AGENDA ---------- */
function addAgenda(){
  const title=document.getElementById('agendaTitle').value.trim();
  const event=document.getElementById('agendaEventSelect').value;
  const desc=document.getElementById('agendaDesc').value.trim();
  if(!title||!event){alert('Fill agenda title and select event');return;}
  const agendas=JSON.parse(localStorage.getItem('agendas')||'[]');
  agendas.push({title,event,desc});
  localStorage.setItem('agendas',JSON.stringify(agendas));
  document.getElementById('agendaTitle').value='';document.getElementById('agendaDesc').value='';
  renderAgenda();
}
function renderAgenda(){
  const div=document.getElementById('agendaList');const agendas=JSON.parse(localStorage.getItem('agendas')||'[]');div.innerHTML='';agendas.forEach(a=>{const d=document.createElement('div');d.className='card';d.innerHTML=`<strong>${a.title}</strong> | ${a.event}<br>${a.desc}`;div.appendChild(d);});
}

/* ---------- BACKUPS ---------- */
function createBackup(){const backups=getBackups();const data={date:new Date().toLocaleString(),users:getUsers(),events:getEvents(),guests:getGuests(),categories:getCategories()};backups.push(data);setBackups(backups);renderBackups();}
function renderBackups(){const ul=document.getElementById('backupList');ul.innerHTML='';getBackups().forEach((b,i)=>{const li=document.createElement('li');li.innerHTML=`<span>${b.date}</span><div><button class="small green" onclick="restoreBackup(${i})">Restore</button> <button class="small danger" onclick="deleteBackup(${i})">Delete</button></div>`;ul.appendChild(li);});}
function restoreBackup(i){const b=getBackups()[i];setUsers(b.users);setEvents(b.events);setGuests(b.guests);setCategories(b.categories);renderAll();alert('Backup restored');}
function deleteBackup(i){const b=getBackups();b.splice(i,1);setBackups(b);renderBackups();}

/* ---------- RENDER ALL ---------- */
function renderAll(){renderCategories();renderEvents();populateEventSelect();populateAgendaEventSelect();renderGuests();renderAgenda();renderAdminStats();}
function renderAdminStats(){document.getElementById('adminUserCount').textContent=getUsers().length;document.getElementById('adminCategoryCount').textContent=getCategories().length;document.getElementById('adminEventCount').textContent=getEvents().length;document.getElementById('adminBackupCount').textContent=getBackups().length;}
