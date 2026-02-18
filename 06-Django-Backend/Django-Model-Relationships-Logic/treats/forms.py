from django import forms

from .models import Human, Child,Ice_Cream,Kiosk

class HumanForm(forms.Form):
    name = forms.CharField(min_length=3,
                           max_length=100,
                           strip=True,
                           required=True,
                           localize=True,
                           label = 'Parent Name',
                           widget=forms.TextInput(attrs={'placeholder': "Enter the name of the new Human parent"}))
    age = forms.IntegerField()
    
    
    pass
class ChildForm(forms.Form):
    name = forms.CharField(min_length=3,
                           max_length=100,
                           strip=True,
                           required=True,
                           localize=True,
                           label = 'Child Name',
                           widget=forms.TextInput(attrs={'placeholder': "Enter the name of the new child "}))
    age = forms.IntegerField()
    parent = forms.ModelChoiceField(
        queryset=Human.objects.all(), 
        label="Select Parent",
        widget=forms.Select()
    )
    pass
class IceCreamForm(forms.Form):
    name = forms.CharField(min_length=3,
                           max_length=100,
                           strip=True,
                           required=True,
                           localize=True,
                           label = 'Ice Cream Name',
                           widget=forms.TextInput(attrs={'placeholder': "Enter the name of the new Ice Cream "}))
    desc = forms.CharField(
        label="Description", 
        widget=forms.Textarea(attrs={'placeholder': "Describe the flavor..."}),
        required=False
    )
    pass
class KioskForm(forms.Form):
    name = forms.CharField(min_length=3,
                           max_length=100,
                           strip=True,
                           required=True,
                           localize=True,
                           label = 'Ice Cream Name',
                           widget=forms.TextInput(attrs={'placeholder': "Enter the name of the new Kiosk "}))
    
    ice_cream_type = forms.ModelChoiceField(
        queryset=Ice_Cream.objects.all(),
        label="Select Ice Cream Type",
        widget=forms.Select()
    )
    address = forms.CharField(label="Kiosk Address")
    pass