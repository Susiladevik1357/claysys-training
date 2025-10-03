interface User {
  id: string;
  name: string;
  email: string;
  role: string;
}

class UserManager {
  private users: User[] = [];

  constructor() {
    this.bindEvents();
  }

  private bindEvents() {
    const addBtn = document.getElementById("add-btn") as HTMLButtonElement;
    const form = document.querySelector("#forms form") as HTMLFormElement;

    // Prevent form submit refresh
    form.addEventListener("submit", (e) => {
      e.preventDefault();
      this.addUser();
    });

    addBtn.addEventListener("click", (e) => {
      e.preventDefault();
      this.addUser();
    });
  }

  private addUser() {
    const nameInput = document.getElementById("name") as HTMLInputElement;
    const emailInput = document.getElementById("email") as HTMLInputElement;
    const roleSelect = document.getElementById("role") as HTMLSelectElement;

    const name = nameInput.value.trim();
    const email = emailInput.value.trim();
    const role = roleSelect.value;

    if (!name || !email || !role) {
      alert("Please fill in all fields.");
      return;
    }

    if (this.users.some((u) => u.email === email)) {
      alert("Email already exists!");
      return;
    }

    const newUser: User = {
      id: Date.now().toString(),
      name,
      email,
      role,
    };

    this.users.push(newUser);
    this.render();

    nameInput.value = "";
    emailInput.value = "";
    roleSelect.value = "";
  }

  private editUser(id: string) {
    const user = this.users.find((u) => u.id === id);
    if (!user) return;

    (document.getElementById("name") as HTMLInputElement).value = user.name;
    (document.getElementById("email") as HTMLInputElement).value = user.email;
    (document.getElementById("role") as HTMLSelectElement).value = user.role;

    this.users = this.users.filter((u) => u.id !== id);
    this.render();
  }

  private deleteUser(id: string) {
    this.users = this.users.filter((u) => u.id !== id);
    this.render();
  }

  private render() {
    const tableBody = document.querySelector(
      "#userTable tbody"
    ) as HTMLTableSectionElement;
    tableBody.innerHTML = "";

    this.users.forEach((user, index) => {
      const row = document.createElement("tr");
      row.innerHTML = `
        <td>${index + 1}</td>
        <td>${user.name}</td>
        <td>${user.email}</td>
        <td>${user.role}</td>
        <td>
          <button class="action edit" data-id="${user.id}">Edit</button>
          <button class="action delete" data-id="${user.id}">Delete</button>
        </td>
      `;
      tableBody.appendChild(row);
    });

    document.querySelectorAll(".edit").forEach((btn) => {
      btn.addEventListener("click", (e) => {
        const id = (e.target as HTMLElement).getAttribute("data-id");
        if (id) this.editUser(id);
      });
    });

    document.querySelectorAll(".delete").forEach((btn) => {
      btn.addEventListener("click", (e) => {
        const id = (e.target as HTMLElement).getAttribute("data-id");
        if (id) this.deleteUser(id);
      });
    });
  }
}

new UserManager();
