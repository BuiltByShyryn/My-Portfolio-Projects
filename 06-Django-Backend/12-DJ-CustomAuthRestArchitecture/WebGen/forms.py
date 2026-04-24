
 
from django.contrib.auth.forms import AuthenticationForm, UserCreationForm
from django.utils.translation import gettext_lazy as _ 
from django import forms 

class FormLogin(AuthenticationForm):
    
    username = forms.CharField(max_length=100, label =_('username'),
                               widget= forms.TextInput({'placeholder': _("enter user name")}))
    password = forms.CharField(max_length=50, label = _('password'),
                               widget= forms.PasswordInput({
                                   'placeholder': _('enter password')
                               }))
    pass