from flask import Flask, render_template, request, redirect

app = Flask(__name__)


FILE_NAME = "data/students.txt"

    
@app.route("/", methods=["GET", "POST"])
def index():

    if request.method == "POST":

        name = request.form["name"]
        age = request.form["age"]
        group = request.form["group"]

        with open(FILE_NAME, "a", encoding="utf-8") as file:
            file.write(f"{name},{age},{group}\n")

        return redirect("/")

    students = []

    with open(FILE_NAME, "r", encoding="utf-8") as file:
        for line in file:
            students.append(line.strip().split(","))

    return render_template("index.html", students=students)

app.run(host="0.0.0.0", port=5000)