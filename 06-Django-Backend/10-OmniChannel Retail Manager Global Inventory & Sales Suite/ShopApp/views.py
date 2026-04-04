from django.shortcuts import render
from django.http import HttpRequest, HttpResponse, Http404, HttpResponseBadRequest
from .models import Brand, Customer, Goods
from django.conf import settings
from .forms import BrandForm, CustomerForm, GoodsForm, BrandSearchForm,CustomerSearchForm


from django.core.files.uploadedfile import UploadedFile, InMemoryUploadedFile
import base64
# Create your views here.
def view_home( req: HttpRequest)-> HttpResponse:
    return render(req, template_name= 'home.html', context={
        'title': "Shop site"
    })
    

def view_brands(req: HttpRequest)-> HttpResponse:
    brand_list = Brand.objects.all()
    return render( req, template_name="brand_list.html", 
                  context={
                      'title': "The list of the brands in the store",
                      "data":brand_list,
                      })
    pass

def view_brand_info(req: HttpRequest, id: int)-> HttpResponse:
    brand_info = Brand.objects.get(pk=id)
    
    return render( req, template_name='brand.html', 
                context={
                    'title': 'Brand Info',
                    'data': brand_info,
                })
    
    
def view_brand_form(req: HttpRequest)-> HttpResponse:
    
    if req.method =='GET':
        form = BrandForm()
        return render(req, template_name='brand_form.html',
                      context={
                          'title': 'Enter the new brand',
                          #'form': form
                          'form': BrandForm()
                      })
        pass
    elif req.method == 'POST':
        print(f'POST data: {req.POST}')
        print (f'FILES:{req.FILES}')
        form = BrandForm(req.POST, files = req.FILES)
        if form.is_valid():
          
            brand = Brand()
            brand.name = form.cleaned_data['name']
            idx = int(form.cleaned_data['country']) - 1
            brand.info = form.cleaned_data['info']
            brand.country = form.country_list[idx][1]
            brand.big_logo = form.cleaned_data['big_logo']
            
            mem_data : InMemoryUploadedFile = form.cleaned_data['small_logo']
            file_data=mem_data.read()
            data64 = base64.b64encode(file_data)
            brand.small_logo = data64
            print(f'daat64: {data64}')
            print(f'decode data64: {data64[:40].decode()}')
            
            
            brand.save()
            
            
            
            return render(req, template_name='brand_form.html',
                      context={
                          'title': 'Enter the data for the next brand',
                          #'form': form
                          'form': BrandForm(),
                          'status': f'Brand {brand.name} was added to the DATABASE',
                      })
            #return redirect ('brands' )
        else:
        
       
             return render(req, template_name='brand_form.html',
                      context={
                          'title': 'Enter the data for the new brand',
                          #'form': form
                          'form': form,
                          'status': f'Error in the data - Check the data',
                      })
    else:
        
        return HttpResponseBadRequest('Error: wrong response')
        pass 

def  view_brand_search(req:HttpRequest)->HttpResponse:
    if req.method == 'POST':
        #ERROR
        return HttpResponseBadRequest('Only Get-requests are Supported  !')
    param = req.GET
    
    if 'text' in param: 
        select_field_name=param.get('select_field', 'name')
        search_text = param['text']
        data = None
        match select_field_name:
            case 'name': 
                #SELECT * FROM Brand AS B WHERE B.name = search_text;
               # data = Brand.objects.filter(name = search_text).all()
            
              
               #SELECT * FROM Brand AS B WHERE B.name LIKE '%{search_text}%'

                data = Brand.objects.filter(name__icontains= search_text).all()
            case 'country':
                data = Brand.objects.filter(country__istartswith = search_text).all()
                pass
        return render(req, template_name='brand_search.html',
                      context={
                          'title': 'Result of the Search' if len(search_text)>0 else 'Search',
                          'title2':"Result of the Search" if len(search_text)>0 else 'List of the Brands',
                          'form': BrandSearchForm(req.GET) ,
                          'data': data,
                      })
        
        
        pass
   # else:
    return render(req,template_name='brand_search.html',
                  context={
                      'title': 'Search',
                      'title2':"The list of the all brands",
                      'form':BrandSearchForm(),
                      'data': Brand.objects.all()
                  })
    pass





def view_goods_form(req: HttpRequest)-> HttpResponse:
    
    
    
    if req.method =="GET":
        return render(req, template_name="goods_form.html",
                      context={
                          'title' : 'Enter good',
                          'header': 'Enter the new good',
                          'form': GoodsForm(),
                      })    
        
        pass
    else:
        if settings.DEBUG is True:
            print('GoodsForm - POST data:',req.POST) 
        form = GoodsForm(req.POST)
        if form.is_valid():
            form.save()
       # if form.non_field_errors:
        #    form.save()
              # ^
              # |
#just a second version of the saving        
        else:
             return render(req, template_name="goods_form.html",
                      context={
                          'title' : 'Enter good',
                          'header': 'Enter the next good',
                          'form': GoodsForm(),
                      }) 
            
        
        pass
    return  render(req, template_name="goods_form.html",
                      context={
                      'title' : 'Enter good',
                      'header': 'Error during the input of the good',
                      'form': form,#form with the errors
                  }) 
    
    pass


def view_customer_form(req: HttpRequest)-> HttpResponse:
    if req.method =="GET":
        form = CustomerForm()
        return render(req, template_name='customer_form.html',
                      context={
                          'title': "Add New Customer",
                          'form': CustomerForm()
                      })
        pass
    elif req.method =="POST":
        print(f"POST data: {req.POST}")
        print(f"FILES: {req.FILES}")
        form = CustomerForm(req.POST, req.FILES)
        if form.is_valid():
            cust = form.save(commit = False)
            
            if 'image' in form.cleaned_data and form.cleaned_data['image']:
                mem_data: InMemoryUploadedFile = form.cleaned_data['image']
                
                file_data = mem_data.read()
                data64 = base64.b64encode(file_data)
                cust.image = data64
                
            cust.save()
            return render(req, template_name='customer_form.html', context={
                'title': "Customer Added!",
                'form': CustomerForm(),
                'status': f"Customer {cust.fio} was successfully saved!"
            })
        else:
            
            print(f"FORM ERRORS: {form.errors}") 
            return render(req, template_name='customer_form.html', 
                          context={
                              'title': "Error in the data",
                              'form': form, 
                              'status': f"Error: {form.errors}", 
                          })
    pass
        
def view_customers(req: HttpRequest)-> HttpResponse:
    customer_list = Customer.objects.all()
    
    return render(req, template_name="customer_list.html",
                  context={
                     'title': 'The list of customers in the store',
                     'data': customer_list, 
                  })
    
def view_cutomer_search(req: HttpRequest)-> HttpResponse:
    if req.method == "POST": 
        return HttpResponseBadRequest('Only Get - responses are supported !')
    param = req.GET
    
    if 'text' in param and param['text'].strip() !="": 
        select_field_name = param.get('select_field', 'name')
        search_text = param['text']
        data = None
        
        match select_field_name:
            case 'name': 
               
                data = Customer.objects.filter(fio__icontains=search_text).all()
            case 'iin':
                
                data = Customer.objects.filter(iin__istartswith=search_text).all()
            case 'phone':
               
                data = Customer.objects.filter(phone__contains=search_text).all()
        return render(req, template_name='customer_search.html',
                      context={
                            'title': 'Result of the Search' if len(search_text)>0 else 'Search',
                          'title2':"Result of the Search" if len(search_text)>0 else 'List of the Brands',
                          'form': CustomerSearchForm(req.GET) ,
                          'data': data,
                      })
    return render(req, template_name='customer_search.html',
                      context={
                        'title': 'Search',
                      'title2':"The list of the all customers",
                      'form':CustomerSearchForm(),
                      'data': Customer.objects.all()
                      })
    pass

def view_customer_info(req: HttpRequest, id:int)-> HttpResponse:
    customer_info = Customer.objects.get(pk = id)
    
    return render(req, template_name='customer.html',
               context={
                   'title': 'Customer Info',
                   'data': customer_info,
               }   )
    
def view_goods(req: HttpRequest)-> HttpResponse:
    goods_list = Goods.objects.all()
    
    return render(req, template_name='good_list.html',
              context={
                  'title': "The list of Goods in the store",
                  'data': goods_list
              }
              ) 
def view_good_info(req: HttpRequest, id:int)-> HttpResponse:
    good_info = Goods.objects.get(pk =id )
    return render(req, template_name='good.html',
                  context={
                      'title': "Good Info",
                      'data': good_info,
                  })


