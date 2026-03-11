//task1
class Bank {
    constructor(loan,deposit){
        this.loan = loan;
        this.deposit = deposit;
    }
    calc_loan(amount,months){
        let monthRate = this.loan/12/100;
        if(monthRate===0){
            return amount / months;
        }
        let payment = (amount * monthRate) / (1 - (1 + monthRate) **-months);
        return payment;
    }
}
const myBank = new Bank(10, 5);
console.log(myBank.calc_loan(100000, 12));
//task2
class Product {
    static store =[];

    constructor(name,price,category){
        this.name = name;
        this.price = price;
        this.category = category;
        Product.store.push(this);
    }
    calc_ForSale(percentage){
        if(percentage <= 0){
            return Error
    }
    let forSakePrice = this.price -(this.price *(percentage /100));
    return forSakePrice;
    }
    find_product(name){
        return Product.store.filter(product => product.name.tolowerCase().includes(name.toLOwerCase()))
    }
    find_porduct_by_category(category){
        return Product.store.filter(product => product.category.tolowerCase()===category.tolowerCase())

    }
}
new Product("iPhone 15", 1000, "Electronics");
new Product("Samsung S23", 900, "Electronics");
new Product("Nike Air Max", 150, "Shoes");
console.log(Product.findProduct("iPhone"));
console.log(Product.findProductByCategory("Electronics"));
console.log(Product.store[0].calcForSale(10));
//task3
const months = {
    January: 31,
    February: 28,
    March: 31,
    April: 30,
    May: 31,
    June: 30,
    July: 31,
    August: 31,
    September: 30,
    October: 31,
    November: 30,
    December: 31
};
for(let month in months){
    console.log(`${month}: ${months[month]} days `)
}
//task4 
class Phone {
    constructor(brand, model, year, price) {
        this.brand = brand;
        this.model = model;
        this.year = year;
        this.price = price;
    }
    getInfo() {
        return `${this.brand} ${this.model}, Year: ${this.year}, Price: ${this.price} `;
    }
    static compare(phone1,phone2){
        if(phone1.price > phone2.price){
            return `${phone1.brand} ${phone1.model} дороже, чем ${phone2.brand} ${phone2.model}`;
        } else if (phone1.price < phone2.price) {
            return `${phone2.brand} ${phone2.model} дороже, чем ${phone1.brand} ${phone1.model}`;
        } else {
            return `${phone1.brand} ${phone1.model} и ${phone2.brand} ${phone2.model} стоят одинаково`;
        }
    }
}

const phone1 = new Phone("Apple", "iPhone 15", 2023, 100000);
const phone2 = new Phone("Samsung", "Galaxy S23", 2023, 90000);
console.log(phone1.getInfo());
console.log(phone2.getInfo());
console.log(Phone.comparePrices(phone1, phone2));

