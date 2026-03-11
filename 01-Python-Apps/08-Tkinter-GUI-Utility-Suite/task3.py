import tkinter as tk 
from tkinter import messagebox


def convert_temperature():
    try:
        temp = float(entry.get())
        if option.get() ==1:
            result = temp *9/5 + 32
            unit = "F"
        elif option.get() == 2:
            result = (temp - 32) * 5/9
            unit = "C"
        else:
            raise ValueError("error")
        messagebox.showinfo("result", f"Temperature:{result:.2f}, {unit}")   
    except ValueError:
        messagebox.showerror("Error", "Type the correct number")
root = tk.Tk()
root.title("Temperature")

Label = tk.Label(root, text="Enter the temperature")
Label.pack(pady=10)

entry =tk.Entry(root, text = "here")
entry.pack(pady = 5)

option = tk.IntVar(value=0)
radio_c_to_f = tk.Radiobutton(root, text="Цельсий -> Фаренгейт", variable=option, value=1)
radio_c_to_f.pack()

radio_f_to_c = tk.Radiobutton(root, text="Фаренгейт -> Цельсий", variable=option, value=2)
radio_f_to_c.pack()

convert_button = tk.Button(root, text="Рассчитать", command=convert_temperature)
convert_button.pack(pady=10)

root.mainloop()