using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MegaChallengeCasino
{
    public partial class Default : System.Web.UI.Page
    {
        Random myRandom = new Random();

        int playerBankAmount;
        int errorCode = 0;
        int storedArrayNumberAssignmentValueOfFirstReel;
        int storedArrayNumberAssignmentValueOfSecondReel;
        int storedArrayNumberAssignmentValueOfThirdReel;
            
        string[] casinoString = new string[] {"Bell", "Clover", "Diamond", "HorseShoe", "Lemon", "Orange", "Plum", "Strawberry", "Watermellon", "Cherry", "Seven", "Bar"};
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Call the method that sets the images to the image URL's initially.
                //call the method that sets the players bank to $100.
                playerBankAmount = 100;
                playerBankLabel.Text = String.Format("{0:C}",playerBankAmount);
                ViewState["playerBankAmount"] = playerBankAmount;

                firstReelResult(casinoString, randomNumberONE());
                secondReelResult(casinoString, randomNumberTWO());
                thirdReelResult(casinoString, randomNumberTHREE());
            }
        }
        
        public void pullLeverButton_Click(object sender, EventArgs e)
        {
           //Outcomes Section:
            int randomNumberOne = randomNumberONE();
            int randomNumberTwo = randomNumberTWO();
            int randomNumberThree = randomNumberTHREE();
            
            firstReelResult(casinoString, randomNumberOne);
            secondReelResult(casinoString, randomNumberTwo);
            thirdReelResult(casinoString, randomNumberThree);
        
            storedArrayNumberAssignmentValueOfFirstReel = randomNumberOne;
            storedArrayNumberAssignmentValueOfSecondReel = randomNumberTwo;
            storedArrayNumberAssignmentValueOfThirdReel = randomNumberThree;
            
           //Potential Win/Loss ("the MULTIPLIER determination") Section:
            int sum = sumsValues(storedArrayNumberAssignmentValueOfFirstReel,storedArrayNumberAssignmentValueOfSecondReel,storedArrayNumberAssignmentValueOfThirdReel);
            int multiplier = rewardsMultiplier(sum);
            
           //Win/Loss Section:
            int bet;
            int WinLossAmount = calculateWinLoss(multiplier, out bet);
            WinLossMessage(WinLossAmount);
           //Player's Bank Section:
           
            int playerBankAmountVS = (int)ViewState["playerBankAmount"];
                if (playerBankAmountVS >= 1)
                {
                playerBankAmount = calculatePlayersBank(playerBankAmountVS, WinLossAmount, bet);
                playerBankLabel.Text = String.Format("{0:C}", playerBankAmount);
                }
                else
                {
                winningsLabel.Text = "Please try again :-)";
                playerBankLabel.Text = "YOU'RE OUT OF MONEY...";
                
                }
            if (errorCode == 1)
            winningsLabel.Text = "Must put only the NUMBER amount of your bet into the 'Your Bet' box...";

            ViewState["playerBankAmount"] = playerBankAmount;         
        }    
        //A way to make this better is to pass in randomNumberONE etc into the input param along with the ones I already have. and eliminate arrayNumberetc.
        //OK, made it better!

        //Outcomes Section Methods:
        public void firstReelResult(string[] imagesString, int randomNumberONE)
        {        
            string imageName = imageNameReturn(imagesString, randomNumberONE);
            firstReelImageFormatter(imageName);
        }

        public void secondReelResult(string[] imagesString, int randomNumberTWO)
        {
            string imageName = imageNameReturn(imagesString, randomNumberTWO);
            secondReelImageFormatter(imageName);
        }
        public void thirdReelResult(string[] imagesString, int randomNumberTHREE)
        {        
             string imageName = imageNameReturn(imagesString, randomNumberTHREE);
             thirdReelImageFormatter(imageName);
        }
        public void firstReelImageFormatter(string imageName)
        {
            //Maybe need two slashes, below... Nope I don't!
            firstImage.ImageUrl = String.Format("Images/{0}.png",imageName);
        }
        public void secondReelImageFormatter(string imageName)
        {
            secondImage.ImageUrl = String.Format("Images/{0}.png",imageName);
        }
        public void thirdReelImageFormatter(string imageName)
        {
            thirdImage.ImageUrl = String.Format("Images/{0}.png",imageName);
        }

        public string imageNameReturn(string[] slotImages, int genericRandomNumber)
        {
            string imageName;
            imageName = slotImages[genericRandomNumber];
            return imageName;
        }
        public int randomNumberONE()
        {
            int value;
            value = myRandom.Next(0,12); 
            return value;
        }

        public int randomNumberTWO()
        {
            int value;
            value = myRandom.Next(0,12); 
            return value;
        }

        public int randomNumberTHREE()
        {
            int value;
            value = myRandom.Next(0,12); 
            return value;
        }


        //Potential Win/Loss ("the MULTIPLIER determination") Section Methods:
        //The method below converts the array results from the results part of the project into an arithmatic value.
        public int convertsArrayNumberResultsToValue(int arrayNumberAssignment)
        {     
            int[] arithValues = new int[12] {0,0,0,0,0,0,0,0,0,5,2,-10};
            int arithmeticValue = arithValues[arrayNumberAssignment];
            return arithmeticValue;     
        }
        //overloads of the method below to pass in values from one to three reels. so, the first two overloads
        //are JUST IN CASE there are less (or methodology if there were more) reels.
        public int sumsValues(int storedArrayNumberAssignmentValueOfFirstReel)
        {
            int sum = convertsArrayNumberResultsToValue(storedArrayNumberAssignmentValueOfFirstReel);
            return sum;
        }

        public int sumsValues(int storedArrayNumberAssignmentValueOfFirstReel, int storedArrayNumberAssignmentValueOfSecondReel)
        {
            int sum = convertsArrayNumberResultsToValue(storedArrayNumberAssignmentValueOfFirstReel) +
                      convertsArrayNumberResultsToValue(storedArrayNumberAssignmentValueOfSecondReel);
            return sum;
        }

         public int sumsValues(int storedArrayNumberAssignmentValueOfFirstReel,int storedArrayNumberAssignmentValueOfSecondReel,int storedArrayNumberAssignmentValueOfThirdReel)
        {
            int sum = convertsArrayNumberResultsToValue(storedArrayNumberAssignmentValueOfFirstReel) +
                     convertsArrayNumberResultsToValue(storedArrayNumberAssignmentValueOfSecondReel) +
                     convertsArrayNumberResultsToValue(storedArrayNumberAssignmentValueOfThirdReel);
            return sum;
        }
         public int rewardsMultiplier(int sum)
         {
             int multiplier; if (sum == 5 || sum == 7 || sum == 9) { multiplier = 2; }
             else if (sum == 10 || sum == 12) { multiplier = 3; }
             else if (sum == 15) { multiplier = 4; }
             else if (sum == 6) { multiplier = 100; }
             else { multiplier = 0; } return multiplier;
         }

         //Win/Loss Section Methods:
        public int calculateWinLoss(int multiplier, out int bet)
         {
             int netWin = 0;
             if(int.TryParse(yourBetTextBox.Text, out bet))
             {
                 netWin = bet * multiplier;       
             }
             else
             errorCode = 1;

             return netWin;
         }
        public void WinLossMessage(int WinLossAmount)
        {
            if (WinLossAmount <= 0)
            winningsLabel.Text = String.Format("Sorry, you lost. Better luck next time.", WinLossAmount);
            else
            winningsLabel.Text = String.Format("You bet {0:C}, and won {1:C}", int.Parse(yourBetTextBox.Text),WinLossAmount);
        }
        //Player's Bank Section Methods:
        public int calculatePlayersBank(int playerBankAmount, int WinLossAmount, int bet)
        {
            //int bet = int.Parse(yourBetTextBox.Text);
            playerBankAmount += WinLossAmount - bet; 
            return playerBankAmount;
        }
        // NOTES:

        //=======================================================================================================================================
        //A big method umbrella here that includes all of the methods up to payout multiplier for an "arm-pull". This method returns
        //   payout multiplier for a pull.  This is to keep the slot machine win success seperate from the slot machine's interaction with the 
        //   players bet and with the player's bank. This method is capped by the pullLeverButtonclick_event and returns the images to the image
        //   controls. So the click event controls every thing about the PULL, up to and including the images, but the numerical values
        //   from the reel returns will be shot up to the top of the program into int variables to be used to calculate
        //   the 'bet' method calculations; this happening outside of the pullLeverClick_Event. 
        //Call a method that calls the sumOfReelImages method below. Call this from this pullLEverButton click event.
        //   The sumOfReelImages collects the numerical returns of the arrays and applies them to the arrayValues of each reel above.
        //Call a method that calls the three methods below into it. This method will be called the "firstReelImageGenerator" method.
        //This method returns or "outs" the number of the image produced.
        //   {
        //   Call a method that generates a random number and assigns it to a value. Params: a discreet instance of a Random.Next().
        //   Returns: a value of that random.

        //   Call a method that assigns a randomly generated value to a string array to produce an image name.
        //   Params: a string[](array), int a value equal to a random.next(11) generation.  Return: String (The image name.)

        //   Call a method that sets one image control's URL to an image name. Params: An image "name".  Return: Void.
        //   }

        //Call a method, the same way as above, called the "secondReelImageGenerator" method.
        //   {Same here}

        //Call a method, the same way as above, called the "thirdReelImageGenerator" method.
        //   {Same here}
        // }


        //Call a method that establishes which combinations of numbers win what amounts.
        //Call one method that assigns number values to the number index from the reel results.
        //  Specifically: The EIGHT carry a value of one. The CHERRY carries a value of two. The SEVEN carries a Three. The BAR a negative 10.
        //         Just add them up. Assign values to the sum accordingly. Params: Array number from above. Return: Its Number value.
        //Call an overload method to add up three number values. Params: Number values. Return: A sum
        //Call another method to assgin to that sum a payout multiplier.
        //Code that posts the payout multiplier to a property-like variable at the top of everything (and maybe persists it with 'ViewState'.


        // {{{{{What other ways could I do this?

        // If...Else conditions, with ||'s and &&'s in the condition.
        // Assign  arithmetic numbers to resulting image numbers. Eight of them carry a value of one.
        //A cherry carries a value of 2.
        //A second cherry carries a value of 2. (But attach a condition where if there is already
        //a cherry in first place then second cherry carries a value of 1.5. A Third cherry
        //carries a value of 2 (unless all first two have cherries in which case, it carries
        // a value of 4/3. (Third cherry has value of 1.5 if only first reel is cherry.)
        // a Seven carries a value of 100 (if either of two others are NOT seven). If one
        // other is a seven then next Seven carries a value of 1/100. Then third bar carries 100 again.
        //A bar carries a zero.
        //GOING to have trouble with this multiplication. eg. Cherry, Cherry, Seven; or Ch, Seven, Other
        //This is because you're assigning a payout value right in the outcomes. You need another step
        // to transition between results and payouts.


        //The EIGHT carry a value of one. The CHERRY carries a value of two. The SEVEN carries a Three. The BAR a negative 10.
        //  Just add them up. Assign values to the sum accordingly. 

        // Use this same principle of assigning values, but use a mulitdimensional array[11,11,11] to organize the results better.
        //   }}}}}

        //Call a SUPERIOR (SUPER-ORDINATE) method that calls the methods responsible for producing the player's winnings.
        //   Call a method that calls the results of the rewards umbrella method.
        //   It multiplies the multiplier to a players bet Params: (yourBetTextBox.Text), multiplier. Return: Winnings
        //   Call a method that produces the result label messages based on win or no-win.
        //   Code that assigns Winnings value to a property-like variable at the top of everything.


        //Call an even SUPERIOR, SUPERIOR (SUPER-DUPER-
        //ORDINATE) umbrella method that calls the methods responsible for managing the player's bank.
        //A method to hold the bet against the players bank. Because you are always going to subtract the bet amount
        //     from the players bank(even if its a win). Param: The bet amount (yourBetTextBox.Text), parse it, and assign to a variable.
        //A Method take the winnings less the bet and produce a totalEarningsForThatRound. Param: PArsed bet value plus winningsvalue.
        //A method that adds the win to the total bank. And produces the playersBankTextBox.Text message.

        //A method that coordinates all resultLabel.Text messages.




    }


}