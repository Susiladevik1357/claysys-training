"use strict";
class UserManager {
    constructor() {
        this.users = [];
        this.bindEvents();
    }
    bindEvents() {
        const addBtn = document.getElementById("add-btn");
        const form = document.querySelector("#forms form");

        form.addEventListener("submit", (e) => {
            e.preventDefault();
            this.addUser();
        });
        addBtn.addEventListener("click", (e) => {
            e.preventDefault();
            this.addUser();
        });
    }
    addUser() {
        const nameInput = document.getElementById("name");
        const emailInput = document.getElementById("email");
        const roleSelect = document.getElementById("role");
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
        const newUser = {
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
    editUser(id) {
        const user = this.users.find((u) => u.id === id);
        if (!user)
            return;
        document.getElementById("name").value = user.name;
        document.getElementById("email").value = user.email;
        document.getElementById("role").value = user.role;
        this.users = this.users.filter((u) => u.id !== id);
        this.render();
    }
    deleteUser(id) {
        this.users = this.users.filter((u) => u.id !== id);
        this.render();
    }
    render() {
        const tableBody = document.querySelector("#userTable tbody");
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
                const id = e.target.getAttribute("data-id");
                if (id)
                    this.editUser(id);
            });
        });
        document.querySelectorAll(".delete").forEach((btn) => {
            btn.addEventListener("click", (e) => {
                const id = e.target.getAttribute("data-id");
                if (id)
                    this.deleteUser(id);
            });
        });
    }
}
new UserManager();
