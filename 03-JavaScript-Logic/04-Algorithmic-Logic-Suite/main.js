//task1

let height = Number(prompt("enter the hegiht in meters:"));
let weight = Number(prompt("enter the weigth in kg:"));

let bmi = weight/(height * hegiht)

alert("your BMI:" + bmi)
//task2
let your_Age = Number(prompt("enter ur age: "));
if (your_Age >= 0 && your_Age <= 12){alert("childhood");}
else if (your_Age >= 13 && your_Age <= 17){alert("Teenage");}
else if (your_Age >= 18 && your_Age <= 64){alert("Adulthood");}
else if (your_Age >=65 ){alert("Retired");}
else {alert("wrong typing");}
//task3 
let a = Number(prompt("ENter the number for a lenght of the triangle"))
let h = Number(prompt("enter the number for a heigth of the triangle:"))

let s = 0.5*a*h

alert("your triangles are:"+ s);
//task4 
let day = Number(prompt("enter the day of birth: "))
let month = Number(prompt("enter the month of the birth:"))

let zedoiac = ""

if ((month === 3 && day >= 21)|| (month === 4 && day <= 20)) {zodiac = "aries";}
else if ((month === 4 && day >= 21)|| (month ===5 && day <= 20)){zodiac = "taurus";}
else if ((month === 5 && day >= 21)|| (month === 6 && day <= 21)){zodiac = "Gemini";}
else if ((month === 6 && day >= 22)|| (month === 7 && day <= 22)){zodiac = "Cancer";}
else if ((month === 7 && day >= 23) || (month === 8 && day <= 23)) {zodiac = "Leo";} 
else if ((month === 8 && day >= 24) || (month === 9 && day <= 23)) {zodiac = "Virgo";}
else if ((month === 9 && day >= 24) || (month === 10 && day <= 23)) {zodiac = "Libra";}
else if ((month === 10 && day >= 24) || (month === 11 && day <= 22)) {zodiac = "Scorpio";}
else if ((month === 11 && day >= 23) || (month === 12 && day <= 21)) {zodiac = "Sagittarius";}
else if ((month === 12 && day >= 22) || (month === 1 && day <= 20)) {zodiac = "Capricorn";}
else if ((month === 1 && day >= 21) || (month === 2 && day <= 20)) {zodiac = "Aquarius";}
else if ((month === 2 && day >= 21) || (month === 3 && day <= 20)) {zodiac = "Pisces";}
else {zodiac = "Invalid date";}
//task5
let f = Number(prompt("Enter the temperature in fahrenheit:"))

let c = (f-32)*5/9;

alert("Temperature in Celcius:")