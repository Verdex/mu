
all : 
	csc Program.cs -out:Game.exe -t:exe

test : Test.exe
	mono Test.exe

Test.exe : 
	csc Grid.cs Location.cs Distance.cs tests.cs -out:Test.exe -t:exe

clean : 
	rm -rf *.exe
