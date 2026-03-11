import tkinter as tk 

def dcounter():
    global counter
    counter += 1
    label.config(text=f"Количество нажатий: {counter}")

root = tk.Tk()
root.title('Clicker')

counter = 0
label = tk.Label(root, text="sum of the clicks: 0")
label.pack(pady = 10)

button = tk.Button(root,text="Click", command=dcounter)
button.pack(pady=5)
root.mainloop()
