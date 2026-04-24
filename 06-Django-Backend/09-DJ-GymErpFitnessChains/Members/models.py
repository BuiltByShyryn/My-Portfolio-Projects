from django.db import models
from django.core. exceptions import ValidationError
from django.core.validators import RegexValidator
from datetime import date 
from django.contrib.auth.models import User
import random as rnd 
import datetime
from datetime import timedelta
# Create your models here.


def CheckIIN(iin:str):
    if len(iin) != 12:
        raise ValidationError('IIn length must be exactly 12!')
    if not iin.isdecimal():
        raise ValidationError("IIN must be contain only numbers")

    yy =int(iin[0:2])
    mm =int(iin[2:4])
    dd = int(iin[4:6])
    century_marker = int(iin[6])
    
    if century_marker in [1,2]: 
        year = 1800 +yy
    elif  century_marker in [3,4]:
        year = 1900+yy
    else: 
        year = 2000 + yy 
        
    try: 
        date(year, mm,dd)
    except ValueError:
        raise ValidationError(f"IIN contains an invalid birth date: {iin[0:6]}")
        
        
    
    summ = sum(map(lambda x: int(x[1]) * x[0], list( enumerate(iin [:11], 1))))
    check_sum = summ %11
    if check_sum == 10:
        check_sum = 0 
    #if check_sum != int(iin[11]): 
    #    raise ValidationError(f'Invalid IIN checksum!')
    pass 

#one of the organisation's Branches or location with the gym 
class GymBranch(models.Model): 
    name  = models.CharField(max_length=100, 
                             unique=True, 
                             verbose_name='Branch Name')
    
    address = models.CharField(max_length=255, 
                               verbose_name='Location')
    
    def __str__(self):
        return self.name
    
    class Meta:
        
        verbose_name = 'Gym Branch'
        verbose_name_plural = 'Gym Branches'

#abonement in the gym for the customers likings
class MembershipPlan(models.Model):
    name =models.CharField(max_length=50, 
                           verbose_name='Plan Name',
                           )
    price = models.DecimalField(max_digits=10, 
                                decimal_places=2,
                                verbose_name='Price')
    duration_months = models.IntegerField(default=1)
    branch = models.ForeignKey(GymBranch, on_delete=models.CASCADE, null=True)
    def __str__(self):
        return f"{self.name}-{self.price} KZT"
    
    class Meta:
       
        verbose_name = 'Membership Plan'
        verbose_name_plural = 'Membership Plans'
    
    
class Member(models.Model):
    user = models.OneToOneField(User, on_delete=models.CASCADE,
                                null = True, blank=True)
    
    first_name = models.CharField(max_length=50,
                                 verbose_name="First Name")
        
    last_name = models.CharField(max_length=50,
                                 verbose_name='Last Name')
    iin = models.CharField(max_length=12, 
                           unique=True, 
                           validators=[CheckIIN],
                           verbose_name='IIN')
    
    photo = models.ImageField(
        upload_to='member_photos/',  
        null=True,
        blank=True,
        verbose_name="Member Photo"
    )
    
    
    branch = models.ForeignKey(GymBranch, 
                               on_delete=models.CASCADE,
                               verbose_name="Home Branch")
    
    plan = models.ForeignKey(MembershipPlan, 
                             on_delete=models.SET_NULL,
                             null=True, 
                             verbose_name='Current Plan')
    
   

    membership_start = models.DateField(auto_now_add=True)
    
    
    
    def save(self, *args, **kwargs):
        self.first_name = self.first_name.strip().capitalize()
        self.last_name = self.last_name.strip().capitalize()
        super().save(*args, **kwargs)
    
    
    
    @classmethod
    def rand_genMembers(self, count: int): 
        branches = GymBranch.objects.all()
        plans = MembershipPlan.objects.all()

        for _ in range(count):
           
            l_name = chr(ord('A') + rnd.randint(0,25)) + "".join([chr(ord('a') + rnd.randint(0,25)) for _ in range(rnd.randint(3,10))])
            f_name = chr(ord('A') + rnd.randint(0,25)) + "".join([chr(ord('a') + rnd.randint(0,25)) for _ in range(rnd.randint(3,10))])
            
            
            current_year = datetime.date.today().year
            year = rnd.randint(current_year - 40, current_year - 16)
            month = rnd.randint(1, 12)
            day = rnd.randint(1, 28)
            temp_date = date(year=year, month=month, day=day)
            
           
            iin_str = temp_date.strftime('%y%m%d') 
            
            
            iin_str += str(rnd.randint(1, 6)) 
            for _ in range(4): 
                iin_str += str(rnd.randint(0, 9))
                
            
            summ = sum(map(lambda x: x[0] * int(x[1]), enumerate(iin_str[:11], 1)))
            check_sum = summ % 11
            if check_sum == 10: check_sum = 0
            iin_str += str(check_sum)

            
            Member.objects.create(
                first_name=f_name,
                last_name=l_name,
                iin=iin_str,
                branch=rnd.choice(branches),
                plan=rnd.choice(plans)
            )
        print(f"Members generated {count}!")
    
   
    def expiration_date(self):
        if self.plan:
            return self.membership_start + timedelta(days=30 * self.plan.duration_months)
        return None
    
    
    
    def __str__(self):
        return f"{self.first_name} {self.last_name} ({self.iin})"
    
    