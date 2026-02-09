import bannerImg from '../assets/Banner.svg'
import Iphone from  '../assets/Iphone.svg'
import './Banner.css'

const Banner = () => {

    return ( 
        <>
        
        <div className="banner">
            <img src={bannerImg} alt="" className='bn'/>
            <img src={Iphone} alt="" className='Imagephone' />
        </div>
        </>
    )
}
export default Banner