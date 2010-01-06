// ============================================================================
// Calculate the value of a planet. 
// 
// The algorithm was taken from a posting in Home World Forum by m.a@stars. The
// original post can be found in the Nova Technical FAQ section of the
// documentation.
// ============================================================================


int PlanetValue(Star star, Race race)
{

#define IMMUNE(a) ((a)==-1)

//simplified for this. Initialized somewhere else

struct playerDataStruct 
{
  int lowerHab[3];	 // from 0 to 100 "clicks", -1 for immunity
  int upperHab[3];
} player;

//in: an array of 3 bytes from 0 to 100
//out: a signed integer between -45 and 100
//hey, it was the Jeffs idea! 
int planetValueCalc(int* planetHabData)
{
  int planetValuePoints=0,redValue=0,ideality=10000;	//in fact, they are never < 0
  int planetHab,habUpper,habLower,habCenter;
  int Excentr,habRadius,margin,Negativ,dist2Center;

  for (int i=0; i<3; i++) {
    habUpper = player.upperHab[i];
    if (IMMUNE(habUpper)) {			//perfect hab
      planetValuePoints += 10000;
    }
    else {	//the messy part
      habLower  = player.lowerHab[i];
      habCenter = (habUpper+habLower)/2;	//no need to precalc
      planetHab = planetHabData[i];

/*
 note: this version makes the basic assumption that habitability is
 symmetrical around the center, that is, the ideal center is located
 in the middle of the lower and upper boundaries, and both halves
 have the same value. The original algorithm seems able to cope with
 weirder definitions, i.e: bottom is 20, top is 80, center is 65,
 and hab value stretches proportionally to the different length of
 both "halves"...
*/

      dist2Center = abs(planetHab-habCenter);
      habRadius = habCenter-habLower;

      if (dist2Center<=habRadius) {		/* green planet */
	Excentr = 100*dist2Center/habRadius;	//note: implicit conversion to integer
	Excentr = 100 - Excentr;		//kind of reverse excentricity
	planetValuePoints += Excentr*Excentr;
	margin = dist2Center*2 - habRadius;
	if (margin>0) {		//hab in the "external quarters". dist2Center > 0.5*habRadius
	  ideality *= (double)(3/2 - dist2Center/habRadius);	//decrease ideality up to ~50%
	/*
	  ideality *= habRadius*2 - margin;	//better suited for integer math
	  ideality /= habRadius*2;
	*/
	}
      } else {					/* red planet */
	Negativ = dist2Center-habRadius;
	if (Negativ>15) Negativ=15;
	redValue += Negativ;
      }
    }
  }

  if (redValue!=0) return -redValue;
  planetValuePoints = sqrt((double)planetValuePoints/3)+0.9;	//rounding a la Jeffs
  planetValuePoints = planetValuePoints * ideality/10000;	//note: implicit conversion to integer

  return planetValuePoints;		//Thanks ConstB for starting this
}


 
 
