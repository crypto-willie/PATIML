# PATIML

1)  SIERRALINK VPS user delete everthing in Documents/NinjaTrader 8/db/
2)  Download Historical Data
3)  Add all indicators, no going back after this step without re-training!
4)  If your using Conner Template, NQ only!  Change AC4 & AC4+ Imbalance Mode to "Horizontal"
5)  We ready to train!
6)  Create model.
7)  Open PATIML.app
8)  Set DATA Seris to 30 Days
9)  Step Only Needed for Model to use in different Time Zones!  Open Ninja Script Editor add // in front of AddPlot(Brushes.Transparent, "TIMEOfDAY"); & compile:
  
   <img src="./Pictures_PATIML/Step1_PATIML.jpg">

10) Goto Strategies, use Conner Template and unCheck "Use PolanityATI Trades" & "Optimize Settings" for 1st part of training.  In picture I name mine PrePack to share w/ friend in differet TimeZone.  Do not get this confuse w/ Conner.
    
    <img src="./Pictures_PATIML/Step2_PATIML.jpg">

11) Enable and watch file grow!  ConnerMLtrain_kjsdfskljdhflsda?????.csv

    <img src="./Pictures_PATIML/Step3_PATIML.jpg">

12) Lets tweak it.  Copy & paste into Documents!  Leave it there, you will need it agian for Regression Training:

    <img src="./Pictures_PATIML/Step5_PATIML.jpg">

    <img src="./Pictures_PATIML/Step6_PATIML.jpg">

13) No Need to playback and train now!  8k lines w/ 65% higher win rate now!  If you remove "Time Of Day"  ML will get better over time!

    <img src="./Pictures_PATIML/Step7_PATIML.jpg">

14) Back to Stratiges:  Check "Use PolanityATI Trades!  Apply!  then Enable!  Apply Again!

    <img src="./Pictures_PATIML/Step4_PATIML.jpg">

15)  Delete or save if you want the ConnerMLtrain_kjsdfskljdhflsda?????.csv.  Copy Paste tweak version into PATIResources folder.  Hit Train:

   <img src="./Pictures_PATIML/Step9_PATIML.jpg">

   See Score w/ indicators? 

16) Reverse Step 14 with Regression.  Switch with Trend in Strategies:  You can do it just like Picture!

  <img src="./Pictures_PATIML/Step10_PATIML.jpg">
    

    
    
