from django.shortcuts import render
from django.http import HttpRequest, HttpResponse, Http404, HttpResponseBadRequest
from .models import Brand, Customer, Goods
from django.conf import settings
from .forms import BrandForm, CustomerForm, GoodsForm, BrandSearchForm,CustomerSearchForm
from django.views.generic import View, TemplateView, DetailView, DeleteView

from django.core.files.uploadedfile import UploadedFile, InMemoryUploadedFile
import base64

class ViewBrandForm(View):
    def __init__(self, **kwargs):
        super().__init__(**kwargs)
    
    
    def get(self, req : HttpRequest)-> HttpResponse:
        return render(req, template_name='brand_form.html',
                      context={
                          'title': 'Enter the new brand',
                          #'form': form
                          'form': BrandForm()
                      })
        
        
        pass
    
    def post(self,req : HttpRequest)->HttpResponse:
        form= BrandForm(req.POST)
        if form.is_valid():
            br = Brand()
            br.name = form.cleaned_data['name']
            br.info  = form.cleaned_data['info']
            id_country =  int(form.cleaned_data['country']) - 1
            br.country = BrandForm.country_list[id_country][1]
            br.big_logo = form.cleaned_data['big_logo']
            br.save()
            return render(req, template_name='brand_form.html',
                      context={
                          'title': 'Enter the data for the next brand',
                          #'form': form
                          'form': BrandForm(),
                          'status': f'Brand {br.name} was added to the DATABASE',
                      })
        
        return render(req, template_name='brand_form.html',
                      context={
                          'title': 'Enter the data for the new brand',
                          #'form': form
                          'form': form,
                          'status': f'Error in the data - Check the data',
                      })
            
        
        
        pass
    def delete(self, req: HttpRequest, id : int)->HttpResponse:
        br_del = Brand.objects.get(pk=id)
        
        pass 
    
    pass 


class ViewGoodsForm(TemplateView):
    
    
    template_name = 'goods_form.html'
    
    
    def __init__(self, **kwargs):
        super().__init__(**kwargs)
        self.context = {'title' : 'Enter good',
                          'header': 'Enter the new good',
                          'form': GoodsForm(),}
        
    def get(self, req : HttpRequest)-> HttpResponse:
        form = GoodsForm()
        return self.render_to_response(
                      context={
                          'title' : 'Enter good',
                          'header': 'Enter the new good',
                          'form': GoodsForm(),
                      })    
        pass
    def post(self, req:HttpRequest)-> HttpResponse:
        form= GoodsForm(req.POST, req.FILES)
        if form.is_valid():
           form.save()
           self.context['header'] = 'Enter the new good'
           self.context['form'] = GoodsForm()
           return self.render_to_response(self.context)  
       
       
        self.context['header'] = 'Error during the input of the good'
        self.context['form'] = form
        return self.render_to_response(self.context) 
    
    pass
    
    