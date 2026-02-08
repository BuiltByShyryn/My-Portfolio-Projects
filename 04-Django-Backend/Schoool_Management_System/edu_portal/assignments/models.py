from django.db import models
from edu_portal.courses.models import Course
from edu_portal.users.models import User

class Assignment(models.Model):
    course = models.ForeignKey(Course, on_delete=models.CASCADE)
    title = models.CharField(max_length=200)
    description = models.TextField()
    deadLine = models.DateTimeField()

    def __str__(self):
        return self.title


class Submission(models.Model):
    assignment = models.ForeignKey(Assignment, on_delete=models.CASCADE)
    student = models.ForeignKey(User, on_delete=models.CASCADE
    , limit_choices_to={'role': 'student'})
    file = models.FileField(upload_to='submissions/')
    submitted_at = models.DateTimeField(auto_now_add=True)
    grade = models.IntegerField()

    def __str__(self):
        return f"{self.assignment.title} - {self.student.username}"