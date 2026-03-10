const TodaysDate = () => {
    const time = new Date()
    console.log(time.getDate(), time.getMonth(), time.getFullYear())
    const day = time.getDate()
    const year = time.getFullYear()
    const month = time.getMonth() + 1
    const daysOff = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];

    return (
        <div className="date">
            {day % 2 === 0 ? `Even:${month}/${year}` : `Odd:${daysOff[time.getDay()]} ${day}`}

        </div>
    )
}
export default TodaysDate