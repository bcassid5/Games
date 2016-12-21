
#include <iostream>
#include <fstream>
#include <string>
#include <unistd.h>
#include <sys/prctl.h>
#include <linux/prctl.h>
#include <sys/signal.h>

using namespace std;

int main() {
    
    
    
    //create the string to be entered by the user when prompted
    string userData;
    //create the file to be opened and printed too
    fstream myFile;
    //create a counter for testing output purposes, as well as for the file names for ease of access
    int counter = 0;
    //another string for the file name
    string fileName;
    
    do {
        //get user input here
        cout << "Please enter a string (typing done will terminate the program): ";
        cin >> userData;
        
        //if its done, skip everything that follows (the fork section) and terminate the program
        if (userData == "done") {
            
            break;
        }
        
        //fork the program here and have the children print to a text file that the string "userData" once every second
        
        counter++;
        pid_t pid = fork();
        prctl(PR_SET_PDEATHSIG, SIGKILL);
        
        //new process, print lines as test
        if (pid == 0) {
            
            
            
            while (userData != "done") {
                
                //determine which file to open
                fileName = to_string(counter) + ".txt";
                //open the process file number
                myFile.open(fileName, std::ios_base::app);
                
                //cout <<endl<< fileName << endl << counter << endl;
                
                
                //reprint the data plus another line to the file
                myFile << userData << "\n";
                
                //close the file
                myFile.close();
                
                //pause for 1 second
                sleep(1);
            }
            
        }
        
        
        
    } while (userData != "done");
    
    
    return 0;
}