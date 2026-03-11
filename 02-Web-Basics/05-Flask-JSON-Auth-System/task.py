import json
import os
from flask import Flask, request, render_template, redirect, url_for

app = Flask(__name__)
USERS_FILE = "users.json"
@app.route("/")
def home():
    return redirect(url_for("register"))


def load_users():
    if not os.path.exists(USERS_FILE):
        return []
    with open(USERS_FILE, "r", encoding="utf-8") as file:
        try:
            return json.load(file)
        except json.JSONDecodeError:
            return []

def save_users(users):
    with open(USERS_FILE, "w", encoding="utf-8") as file:
        json.dump(users, file, indent=4)

@app.route("/register", methods=["GET", "POST"])
def register():
    if request.method == "POST":
        data = request.form
        users = load_users()

        if any(user["login"] == data["login"] for user in users):
            return "Логин уже используется"
        
        if data["password"] != data["confirm_password"]:
            return "Пароли не совпадают"
        
        users.append({
            "name": data["name"],
            "surname": data["surname"],
            "login": data["login"],
            "password": data["password"]
        })
        save_users(users)
        return redirect(url_for("users"))
    return render_template("register.html")

@app.route("/users")
def users():
    users = load_users()
    return render_template("users.html", users=users)

if __name__ == "__main__":
    app.run(debug=True)
