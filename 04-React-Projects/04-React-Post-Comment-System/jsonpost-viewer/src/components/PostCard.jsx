import { useEffect, useState } from 'react'
import './PostCard.css'

const PostCard = () => {
const api = "https://jsonplaceholder.typicode.com/posts/1"
const[userInfo,setUserInfo] = useState(
    {}
)
useEffect(() => {
    fetch(api).then((res) => (res.json())).then ((userInfo) => (setUserInfo(userInfo)))
},[])
const {userId,id,title,body} = userInfo
const [showComments, setShowComments] = useState(false)
const api_comments='https://jsonplaceholder.typicode.com/comments?postId=1'
const[comments,setComments] = useState([])
useEffect(()=> {
    fetch(api_comments).then((res)=>(res.json())).then((comments)=>(setComments(comments)))
},[])


    return (
        <>
            <div className="post">

                <div className="container">
                    <span>UserId: {userId}</span>
                    <span>ID: {id}</span>
                    <span>Title: {title}</span>
                    <span>Description: {body}</span>
<button className="com" onClick={() => setShowComments(!showComments)}>
            {showComments ? 'Hide Comments' : 'Show Comments'}
          </button>
 {showComments && (
            <div className="comments">
              {comments.map(comment => (
                <div key={comment.id} className="comment">
                  <p><strong>{comment.name}</strong></p>
                  <p>{comment.body}</p>
                  <p><em>{comment.email}</em></p>
                </div>
              ))}
            </div>
          )}
                </div>
            </div>
        </>
    )
}
export default PostCard