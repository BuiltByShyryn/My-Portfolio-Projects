from django.db import models
from django.core.validators import RegexValidator, \
    MinValueValidator,MaxValueValidator,\
        MinLengthValidator,MaxLengthValidator
  
from django.core.exceptions import ValidationError
import random as rnd
from datetime import date



def CheckIIN(iin : str):
    if len(iin) != 12:
        raise ValidationError('IIN lenght must  be  < 12 ')
    if not iin.isdecimal():
        raise ValidationError("IIN must contain only numbers!")
    
    
    
    yy= int(iin[0:2])
    mm = int(iin[2:4])
    dd = int(iin[4:6])
    
    

    century_marker = int(iin[6])
    if century_marker in [1, 2]:
        year = 1800 + yy
    elif century_marker in [3, 4]:
        year = 1900 + yy
    else:
        year = 2000 + yy

    try:
       
        date(year, mm, dd)
    except ValueError:
        raise ValidationError(f"IIN contains an invalid birth date: {iin[0:6]}")
   
    summ = sum(int(digit) * i for i, digit in enumerate(iin[:11], 1))
    
    check_sum = summ % 11
    if check_sum ==10:
        check_sum=0
        
    if check_sum != int(iin[11]):
        raise ValidationError(f"Wrong summirised controlling amount of IIN check_sum ={check_sum}")
    pass

class IIN_Validator():
    def __init__(self, iin_len : int):
        self.iin_len = iin_len
        pass
    def __call__(self, iin:str):
        self.iin = iin
        pass
    
    pass


class Group( models.Model ):
    name = models.CharField( max_length=20, unique=True,
                             verbose_name='Название группы' )
    icon = models.ImageField( verbose_name='Иконка группы', upload_to='',
                              null=True, blank=True )
    
    def __str__(self):
        return f'{self.name}'
    @classmethod
    def rand_genGroup(self):#gen group
        while True:
            name = ''
            for x in range(3):
                name +=chr(ord('a') + rnd.randint(23))
            name += '-' +str(rnd.randint(100,999))
            print('Generate group: ', name ) # for otladka 
            if Group.objects.filter(name = name).count() == 0:
                Group(name = name).save()
                return
            pass
        
    
    
    #gen groups amount
    def rand_Groups(self, count : int):
        for i in range(count):
            self.rand_genGroup()
        
        pass

    
    class Meta:
        managed = True
        verbose_name        = 'Группа'
        verbose_name_plural = 'Группы'    
    
    
    
    
    pass # class Groups( models.Model )

class Course( models.Model ):
    name = models.CharField( max_length=100, verbose_name='Название курса',
                             unique=True )
    info = models.TextField( verbose_name='Описание курса', null=True, blank=True)
    icon = models.ImageField( verbose_name='Иконка курса',
                              upload_to='',
                              null=True, blank=True)
    pass  # class Courses( models.Model )

    def __str__(self):
        return f'{self.name}'
    
    class Meta:
        db_table = ''
        managed = True
        verbose_name = 'Course'
        verbose_name_plural = 'Courses'


class Student(models.Model):
    last_name = models.CharField(max_length=20, verbose_name="Surname", 
                                 db_index=True,
                                 validators=[RegexValidator(r'^[A-ZА-Я][a-zа-я-]*$')])
    first_name = models.CharField(max_length=20, verbose_name="Name", 
                                  validators=[RegexValidator(r'^[A-ZА-Я][a-zа-я-]*$')])
    iin = models.CharField(max_length=12, verbose_name="IIN", db_index=True, validators=[CheckIIN])
    bdate = models.DateField(verbose_name='Birth date', null=True, blank=True)
    group_id = models.ForeignKey('Group', on_delete=models.CASCADE)

    def __str__(self):
        return f'{self.last_name} {self.first_name[0]}. {self.group_id}'

    def rand_genOneStudent(self):
        
        last_name = chr(ord('A') + rnd.randint(0, 25)) + "".join([chr(ord('a') + rnd.randint(0, 25)) for _ in range(rnd.randint(3, 10))])
        first_name = chr(ord('A') + rnd.randint(0, 25)) + "".join([chr(ord('a') + rnd.randint(0, 25)) for _ in range(rnd.randint(3, 10))])

        year_val = rnd.randint(1990, 2008)
        bdate_val = date(year_val, rnd.randint(1, 12), rnd.randint(1, 28))
        iin_str = bdate_val.strftime("%y%m%d") + "".join([str(rnd.randint(0, 9)) for _ in range(5)])

        
        s = sum(int(d) * i for i, d in enumerate(iin_str, 1))
        iin_str += str(s % 11 if s % 11 < 10 else 0)

       
        gr_list = Group.objects.all()
        if gr_list.exists():
            Student.objects.create(
                last_name=last_name, 
                first_name=first_name, 
                iin=iin_str[:12], 
                bdate=bdate_val, 
                group_id=rnd.choice(gr_list)
            )

    class Meta:
        verbose_name = 'Student'
        verbose_name_plural = 'Students'
class Exam(models.Model):
    course_id = models.ForeignKey('Course', on_delete=models.CASCADE,
                                  verbose_name='Course')
    student_id =models.ForeignKey('Student', on_delete=models.CASCADE,
                                  verbose_name='Student')
    grade = models.PositiveSmallIntegerField(verbose_name='Assestment',
                                             validators=[
                                                 MinValueValidator(1),
                                                 MaxValueValidator(12)
                                             ])
    date = models. DateTimeField(verbose_name='Exam Date')
    fee = models.FloatField(verbose_name='The exam fee',
                            default= 0)
    
    def __str__(self):
        return f'{self.student_id} -{self.course_id} - {self.grade}'
    class Meta:
        db_table = ''
        managed = True
        verbose_name = 'Exam'
        verbose_name_plural = 'Exams'
    
    def rand_genExams(self, count: int):
        students = Student.objects.all()
        courses = Course.objects.all()
        if not students.exists() or not courses.exists():
            print("Add students and courses first!")
            return
            
        for _ in range(count):
            Exam.objects.create(
                student_id=rnd.choice(students),
                course_id=rnd.choice(courses),
                grade=rnd.randint(1, 12),
                date=date.today(),
                fee=rnd.randint(0, 5000)
            )

