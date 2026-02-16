let points = 0, clickPower = 1, upgradeCost = 10;

const clickBtn = document.querySelector("#clickBtn");
const upgradeBtn = document.querySelector("#upgradeBtn");
const pointsDisplay = document.querySelector("#points");
const clickPowerDisplay = document.querySelector("#clickPower");
const upgradeCostDisplay = document.querySelector("#upgradeCost");

clickBtn.onclick = () => {
    points += clickPower;
    updateUI();
};

upgradeBtn.onclick = () => {
    if (points >= upgradeCost) {
        points -= upgradeCost;
        clickPower *= 1.05;
        upgradeCost = Math.ceil(upgradeCost * 1.15);
    }
    updateUI();
};

function updateUI() {
    pointsDisplay.innerText = points;
    clickPowerDisplay.innerText = clickPower.toFixed(2);
    upgradeCostDisplay.innerText = upgradeCost;
    upgradeBtn.disabled = points < upgradeCost;
}
