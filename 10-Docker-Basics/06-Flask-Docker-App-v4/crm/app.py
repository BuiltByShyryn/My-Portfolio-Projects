from flask import Flask, render_template, request, redirect
import os

app = Flask(__name__)

FILE_NAME = "data/employees.txt"

if not os.path.exists(FILE_NAME):
    with open(FILE_NAME, "w", encoding="utf-8"):
        pass

@app.route("/", methods=["GET", "POST"])
def index():
    if request.method == "POST":
        name = request.form["name"]
        position = request.form["position"]
        department = request.form["department"]
        salary = request.form["salary"]

        with open(FILE_NAME, "a", encoding="utf-8") as file:
            file.write(f"{name}|{position}|{department}|{salary}\n")
        return redirect("/")

    employees = []
    with open(FILE_NAME, "r", encoding="utf-8") as file:
        for line in file:
            employees.append(line.strip().split("|"))
    return render_template("index.html", employees=employees)

@app.route("/clear", methods=["POST"])
def clear():
    with open(FILE_NAME, "w", encoding="utf-8"):
        pass
    return redirect("/")


app.run(host="0.0.0.0", port=5000)