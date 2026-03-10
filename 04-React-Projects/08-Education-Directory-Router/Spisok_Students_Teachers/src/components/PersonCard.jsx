import './PersonCard.css'
const PersonCard = ({ fullName, age, course, subject, phone, email }) => {
  return (
    <div className="card">
      <h3>{fullName}</h3>
      <p>Age: {age}</p>
      {course && <p>Course: {course}</p>}
      {subject && <p>Subject: {subject}</p>}
      <p>Phone: {phone}</p>
      <p>Email: {email}</p>
    </div>
  );
};

export default PersonCard;
