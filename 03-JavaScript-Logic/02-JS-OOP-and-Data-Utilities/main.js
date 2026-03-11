//task1
class Building{
    constructor(height, width, length, floors, hasBasement) {
        this.height = height;
        this.width = width;
        this.length = length;
        this.floors = floors;
        this.hasBasement = hasBasement;
    }
    getarea(){
        return this.width * this.length * this.floors;
    }
    addfloor(){
        this.floors++;
    }
    removeFloor(){
        if(this.floors >1 ){
            this.floors --;
        }else{
            console.log("cannot delete the floor ")
        }
    }
}
//task2
const Company = {
    name: "techCorp",
    employes: [
        { name: "Алиса", position: "Разработчик", salary: 150000 },
        { name: "Боб", position: "Дизайнер", salary: 120000 },
        { name: "Чарли", position: "Менеджер", salary: 130000 }
    ],
    getAverageSalary() {
        if (this.employees.length === 0) return 0;
        let total = this.employees.reduce((sum, employee) => sum + employee.salary, 0);
        return total / this.employees.length;
    }
}
console.log(company.getAverageSalary());
//task 3
const Client = {
    name: "alice",
    address: "Wonderland",
    phone:"none",
    is_here(){
        for(let key of ["name", "address", "phone"]){
            if(!this[key]){
                console.log("error none any of these keys are exist");
                return false;
            }
    
        }
    return true;
    }

}
console.log(client.is_here());