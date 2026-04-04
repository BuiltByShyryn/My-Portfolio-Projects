from django.contrib import admin
from .models import GymBranch, MembershipPlan, Member
# Register your models here.

admin.site.register([GymBranch,MembershipPlan,Member])


