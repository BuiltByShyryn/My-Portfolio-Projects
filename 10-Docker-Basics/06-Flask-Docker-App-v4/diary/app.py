from flask import Flask, render_template, request, redirect
import os

app = Flask(__name__)

FILE_NAME = "data/diary.txt"

if not os.path.exists(FILE_NAME):
    with open(FILE_NAME, "w", encoding="utf-8"):
        pass
    
@app.route("/", methods=["GET", "POST"])
def index():

    if request.method == "POST":

        title = request.form["title"]
        text = request.form["text"]

        with open(FILE_NAME, "a", encoding="utf-8") as file:
            file.write(f"{title}|{text}\n")

        return redirect("/")

    notes = []

    with open(FILE_NAME, "r", encoding="utf-8") as file:
        for line in file:
            notes.append(line.strip().split("|"))

    return render_template("index.html", notes=notes)


app.run(host="0.0.0.0", port=5000)