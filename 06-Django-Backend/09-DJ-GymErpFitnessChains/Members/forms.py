from django import forms
from .models import Member
from django.contrib.auth.models import User
from .models import MembershipPlan

class MemberForm(forms.ModelForm):
    class Meta:
        model = Member
        
        fields = ['first_name', 'last_name', 'iin', 'branch', 'plan','photo']
        
        
        widgets = {
            'first_name': forms.TextInput(attrs={'class': 'form-control'}),
            'last_name': forms.TextInput(attrs={'class': 'form-control'}),
            'iin': forms.TextInput(attrs={'class': 'form-control', 'placeholder': '12 digits'}),
        }
        help_texts = {
            'first_name': 'First letter must be uppercase (e.g., Anatoly)',
            'last_name': 'First letter must be uppercase (e.g., Toxheanatoly)',
            'iin': 'Enter 12 digits, must be unique',
        }
        
class LoginForm(forms.Form): 
    username = forms.CharField()
    password = forms.CharField(widget=forms.PasswordInput)
    
class CreateUserForm(forms.ModelForm): 
    
    password = forms.CharField(widget=forms.PasswordInput)
    email = forms.EmailField(required=False, label="Email Address (Optional)") 

    class Meta:
        model = User
        fields = ['username', 'password', 'email']
        help_texts = {
            'username': None
        }
        
        


class MembershipPlanForm(forms.ModelForm):
    class Meta:
        model = MembershipPlan
        fields = ['name', 'price', 'duration_months', 'branch']  
        widgets = {
            'branch': forms.Select(attrs={'class': 'form-control'}),  
        }