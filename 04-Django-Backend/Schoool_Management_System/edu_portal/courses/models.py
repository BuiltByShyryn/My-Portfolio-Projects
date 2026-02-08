from django.db import models

# Create your models here.
from edu_portal.users.models import User

class Course(models.Model):
    title = models.CharField(max_length=100)
    description = models.TextField()
    teacher = models.ForeignKey(User,on_delete=models.CASCADE,
                                limit_choices_to={'role': 'teacher'},)

    def __str__(self):
        return self.title

class Group(models.Model):
    course = models.ForeignKey(Course,on_delete=models.CASCADE)
    student = models.ManyToManyField(User,limit_choices_to={'role': 'student'},)
    start_date = models.DateField()

    def __str__(self):
        return f'{self.course.title}-Group {self.id}'
