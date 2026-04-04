from django.shortcuts import render
from django.http import HttpRequest, HttpResponse
from django.views.generic import TemplateView
# Create your views here.
from .models import Group, Student
from django.core.paginator import Paginator
from django.shortcuts import render, redirect, get_object_or_404
class VGroups(TemplateView):
    
    
    template_name = 'groups.html'
    def get(self, req : HttpRequest, *args, **kwargs):
        
        groups = Group.objects.all()
        return self.render_to_response({
            'title' : 'List of Groups',
            'data' : groups,
        })

class VStudents(TemplateView):
    template_name= 'students.html'
    
    def get(self, req: HttpRequest, *args, **kwargs):
        all_students = Student.objects.all().order_by('last_name')
        
       
        paginator = Paginator(all_students, 10) 
        
        
        page_number = req.GET.get('page')
        page_obj = paginator.get_page(page_number)
        
        return self.render_to_response({
            'title': 'List of Students',
            'page_obj': page_obj  
        })
        
class VrandGroup(TemplateView):
    template_name= 'groups_rand_form.html'
    def get(self, req: HttpRequest, *args, **kwargs):
        return self.render_to_response({
            'title': 'Generation of random groups',
            'form' : FormGenGroup(),
        })
        
    def post(self, req: HttpRequest):
        form = FormGenGroup(req.POST)
        if form.is_valid():
            count = int(form.cleaned_data['count'])
            group = Group()
            group.rand_Groups(count)
            return redirect( 'group_list')

        return self.render_to_response({
            'title' : "Generation of random groups",
            'form': form,
            'status' :  "Error on the  form! Fix it!"
        })
            
            
        pass
        
class VStudentDetail(TemplateView):
    template_name = 'student_detail.html'
    def get(self, req, pk): 
        student = Student.objects.get(id=pk)
        return self.render_to_response({'student': student})