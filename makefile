
all : 
	csc Program.cs -out:Game.exe -t:exe

clean : 
	rm -rf *.exe
