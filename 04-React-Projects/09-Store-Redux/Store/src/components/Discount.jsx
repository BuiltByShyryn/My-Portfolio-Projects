import './Discount.css'
import { useState } from 'react'



const Discount = () => {
    const slider = [
        "discount_1.svg", "discount_2.svg",
        "discount_3.svg",
        "discount_4.svg",
        "discount_5.svg",
        "discount_6.svg", "discount_7.svg"
        , "discount_8.svg", "discount_9.svg"
    ]

    const [currentIndex, setCurrentindex] = useState(0);

    return (
        <>
            <section>
                <div className="discounts">
                    Discounts:
                    <div className="container">
                        <div className="discounts__images">
                            <button
                                
                                onClick={() =>
                                    setCurrentindex(prev =>
                                        prev === slider.length - 1 ? 0 : prev + 1
                                    )
                                }
                            >‹</button>

                            <img src={slider[currentIndex]} alt="discount" />


                            <button  onClick={() => setCurrentindex(prev =>
                                prev === 0 ? slider.length - 1 : prev - 1
                            )
                            }
                            >›</button>
                        </div>


                    </div>
                </div>
            </section>
        </>
    )
}
export default Discount