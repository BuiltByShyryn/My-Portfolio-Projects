import bannerImg from '../assets/banner.svg'

const Banner = () => {
    return (
        <div className="banner">
            <div className="container">
                <img src={bannerImg} alt="" />
            </div>
        </div>
    )
}

export default Banner;