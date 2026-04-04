from django import forms 

from django.core import validators

from .models import Brand, Customer, Sales,Goods

class BrandForm(forms.Form):
    name = forms.CharField(min_length=3,
                           max_length=100,
                           strip=True,
                           required=True,
                           label='Brand Name',
                           localize=True,
                          widget=forms.TextInput(attrs={"placeholder": "Input the name of the brand"})
                           
                           )
    country_list = [(1,"USA"),(2,"China"),(3, "Kazakhstan"),(4,"Russia")]
    country = forms.ChoiceField( choices =country_list,
                                required=True,label = "Country Name",
                                initial=country_list[2],
                                widget=forms.Select())
    info = forms.CharField(
        max_length=1000,
        widget=forms.widgets.Textarea(),
        required=False,
        label ='Info about brand'
    )
    
    big_logo = forms.ImageField(max_length=256,
                             allow_empty_file=True,
                             required=False,
                             label="Brand Image")
    small_logo = forms.FileField(max_length=20*1024,
                                 allow_empty_file=False,
                                 required=False,
                                 label="Brand Icon")
    pass

class GoodsForm(forms.ModelForm):
    
    cat_list = [('I', 'Clothes'),
                ('II', 'Electronics'),
                ('III', 'Furniture'),
                ('IV', 'Goods'),
                ('V', 'Toys')
                
                ]
    
    category = forms.ChoiceField( choices=cat_list)
    #validator
    def clean_price( self)->float:
        val = self.cleaned_data['price']
        print('val=',val)
        
        #temp = validators.MinValueValidator(1)
        #temp(val)
        if val< 1:
            raise forms.ValidationError('price < 1 !', code = '123')
        if val >1_000_000:
            raise forms.ValidationError("price >1 000 000", code = '124')
        return val 
        pass
    def clean(self)-> float:
       
        pass
    
   
   
   
   
    class Meta:
       model = Goods
       #fields = '__all__'
       fields = ('id_brand', 'name', 'price', 'count','info', 'category')
       labels = {'id_brand': 'Brand Name',
                 'name': "Good Name",
                 'price': "Price",
                 'count': 'Amount',
                 'info': "Information",
                 'category': 'Category of the Good'}
       initials = {'count': 10,
                   'category' : ('III', 'Furniture')}
       
       
       validators = {
           'price' : [ validators.MinValueValidator(1, 'The price shouldnt be null!'),
                    validators.MaxValueValidator(1_000_000, "The price sohuld lower than 1 000 000 tg!"),
                  
                      ],
           'name': [
                 validators.MinLengthValidator(5, "The name should ahve mre letters  than 5 "),
                validators.RegexValidator(r'[A-ZА-Я]w+',
                                          "The name should have Imperical Letter!")
           ]
       }
       
       
       pass
    
    pass

class CustomerForm(forms.ModelForm):
    fio = forms.CharField(min_length=3,
                          max_length=100,
                          strip=True,
                          required=True,
                          label="Customer FIO",
                          localize=True,
                          widget=forms.TextInput(attrs={'placeholder': 'Enter new Custoemr '}))
    
    iin = forms.CharField(max_length=250,
                          strip=True,
                          label="Customers IIN",
                          localize=True,
                          widget=forms.TextInput(attrs={'placeholder': "Enter the IIN numbers"}))
    email = forms.CharField(required=False,max_length=255)
    phone = forms.CharField(required=False,max_length=255)
    image = forms.ImageField(required=False, label="Upload Photo")
    
    class Meta:
        model = Customer 
        fields = ('fio', 'iin', 'image')
        labels = {'fio': 'Full Name',
                  
                  'iin': 'Citizen Identification',
                  
                  
                  'image': "Image"}
        

    pass
  
class BrandSearchForm(forms.Form):
    select_list = [('name', 'Name'),('country','Country')]
    select_field = forms.ChoiceField(label = 'Choose search field',
                                     choices = select_list,
                                     widget=forms.widgets.Select(),
                                     
                                     )
    text = forms. CharField(max_length=200, label = 'Search field', strip=True,
                            widget=forms.widgets.Input(attrs={
                                'placeholder': 'Enter the text for a search'
                            }))
    
    
    pass


class CustomerSearchForm(forms.Form):
    select_list = [('name', 'FIO'),('iin', 'IIN'),('phone', 'Phone')]
    select_field = forms.ChoiceField(label = 'Choose search field',
                                     choices = select_list,
                                     widget=forms.widgets.Select(),
                                     )
    text = forms.CharField(max_length=256, label = "Search Field", 
                           strip = True,
                           widget = forms.widgets.Input(attrs={
                               'placeholder' : "Enter the text for a research"
                               }))
   
   
    #Validator
   
  #  def clean_text(self):
   #     data = self.cleaned_data.get('text', '').strip()
    #    select_field = self.cleaned_data.get('select_field')
#
       
    #    if select_field == 'iin':
    #        if len(data) != 12 or not data.isdigit():
    #            raise forms.ValidationError("IIN must be exactly 12 digits.")

       
     #   elif select_field == 'phone':
    #        if not data.startswith('+') and len(data) > 0:
            
    #            pass 
                
   #     return data

    def clean_text(self):
        data = self.cleaned_data.get('text', '').strip()
        
        return data