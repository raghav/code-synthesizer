from django.shortcuts import render
from django.http import HttpResponse, JsonResponse
import os
from django.conf import settings
from django.views.decorators.csrf import csrf_exempt
import os
import codecs


# Create your views here.
@csrf_exempt
def index(request):
	if(request.method == 'GET'):
		
		return render(request, 'pix2code/index.html')
	else:
		#call the class
		file = request.FILES['myFile']
		type_code = request.POST.get('type',"")
		if type_code == 'android':
			path=os.path.dirname(os.path.abspath(__file__))+"/AI_android/model/"

			destination = open(settings.BASE_DIR + "/media/image", "wb")
			destination.write(file.read())
			destination.close()
			
			generate_gui="CUDA_VISIBLE_DEVICES="" python "+path+"sample.py "+path+"../bin pix2code "+settings.BASE_DIR + "/media/image "+path+"../code/"+type_code+" greedy"
			print("CHECK",generate_gui)
			
			os.system(generate_gui)

			path_compile=os.path.dirname(os.path.abspath(__file__))+"/AI_android/compiler/"
			dsl_code="python "+path_compile+"android-compiler.py "+path+"../code/"+type_code+"/imag.gui"
			os.system(dsl_code)

			if type_code=="android":
				f=codecs.open(path+"../code/android/imag.xml", 'r')
				return HttpResponse(f.read())
		elif type_code == 'windows':
			path=os.path.dirname(os.path.abspath(__file__))+"/AI_android/model/"

			destination = open(settings.BASE_DIR + "/media/image", "wb")
			destination.write(file.read())
			destination.close()
			
			generate_gui="CUDA_VISIBLE_DEVICES="" python "+path+"sample.py "+path+"../bin pix2code "+settings.BASE_DIR + "/media/image "+path+"../code/"+type_code+" greedy"
			print("CHECK",generate_gui)
			
			os.system(generate_gui)

			path_compile=os.path.dirname(os.path.abspath(__file__))+"/AI_android/compiler/"
			dsl_code="python "+path_compile+"windows-compiler.py "+path+"../code/"+type_code+"/imag.gui"
			os.system(dsl_code)

			if type_code=="windows":
				f=codecs.open(path+"../code/windows/imag.xaml", 'r')
				return HttpResponse(f.read())


		else:
			path=os.path.dirname(os.path.abspath(__file__))+"/AI/model/"

			destination = open(settings.BASE_DIR + "/media/image", "wb")
			destination.write(file.read())
			destination.close()
			
			generate_gui="CUDA_VISIBLE_DEVICES="" python "+path+"sample.py "+path+"../bin pix2code "+settings.BASE_DIR + "/media/image "+path+"../code/"+type_code+" "+type_code+" 3"
			print("CHECK",generate_gui)
			
			os.system(generate_gui)

			path_compile=os.path.dirname(os.path.abspath(__file__))+"/AI/compiler/"
			dsl_code="python "+path_compile+"web-compiler.py "+path+"../code/"+type_code+"/imag.gui"
			os.system(dsl_code)

			if type_code=="web":
				f=codecs.open(path+"../code/web/imag.html", 'r')
				return HttpResponse(f.read())

		# type_code = "web"
		# generate_gui="CUDA_VISIBLE_DEVICES="" python sample.py ../bin/"+type_code+" pix2code ../datasets/"+type_code+"/eval_set/00190F39-0DE9-47EB-B0C2-856FDD3ACE62.png ../code/"+type_code+" "+type_code+" 3"
		# os.system(generate_gui)
		data="Check"
		return HttpResponse(data)
