from django import forms

from .models import Human, Child,Ice_Cream,Kiosk

class HumanForm(forms.ModelForm):
    name = forms.CharField(min_length=3,
                           max_length=100,
                           strip=True,
                           required=True,
                           localize=True,
                           label = 'Parent Name',
                           widget=forms.TextInput(attrs={'placeholder': "Enter the name of the new Human parent"}))
    age = forms.IntegerField()
    class Meta:
        model = Human
        fields = ['name', 'age']
    
    pass
class ChildForm(forms.ModelForm):
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
    
    class Meta:
        model = Child
        fields = ['name', 'age', 'parent']
    
    pass
class IceCreamForm(forms.ModelForm):
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
    
    class Meta:
        model = Ice_Cream
        fields = ['name', 'desc']
    
    pass
class KioskForm(forms.ModelForm):
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
        required=False,
       
        widget=forms.Select()
    )
    address = forms.CharField(label="Kiosk Address")
    class Meta:
            model = Kiosk
            fields = ['name', 'ice_cream_type', 'address']

    pass

