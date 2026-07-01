from flask import Flask, render_template

app = Flask(__name__)

@app.route("/")
def home():
    return render_template("index.html")
@app.route("/about")
def about():
    return render_template("about.html")

@app.route("/docker")
def docker():
    return render_template("docker.html")

@app.route("/http")
def http():
    return render_template("http.html")

@app.route("/https")
def https():
    return render_template("https.html")

@app.route("/localhost")
def localhost():
    return render_template("localhost.html")

@app.route("/request")
def request():
    return render_template("request.html")

app.run(host="0.0.0.0", port=5000)