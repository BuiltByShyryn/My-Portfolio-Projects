from flask import Flask, render_template, request, redirect
import os

app = Flask(__name__)

FILE_NAME = "data/bookings.txt"

if not os.path.exists(FILE_NAME):
    with open(FILE_NAME, "w", encoding="utf-8"):
        pass


@app.route("/", methods=["GET", "POST"])
def index():

    if request.method == "POST":
        first_name = request.form["first_name"]
        last_name = request.form["last_name"]
        seat = request.form["seat"]

        with open(FILE_NAME, "r", encoding="utf-8") as file:
            for line in file:
                booking = line.strip().split("|")

                if booking[2] == seat:
                    return redirect("/")

        with open(FILE_NAME, "a", encoding="utf-8") as file:
            file.write(f"{first_name}|{last_name}|{seat}\n")
        return redirect("/")

    bookings = []
    with open(FILE_NAME, "r", encoding="utf-8") as file:
        for line in file:
            bookings.append(line.strip().split("|"))
    return render_template("index.html", bookings=bookings)

app.run(host="0.0.0.0", port=5000)