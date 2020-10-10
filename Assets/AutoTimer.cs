using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// A timer decides whether a certain timecap has been reached.
class AutoTimer
{
    // The cap for this timer
    private double timeCap;
    private double originalTimeCap;

    // Constructor function for a Timer which takes in a timeCap
    public AutoTimer(double timeCap)
    {
        originalTimeCap = timeCap;
        this.timeCap = timeCap;
    }

    // Checks whether a timecap has been reached
    // Returns true if it has, false otherwise.
    public bool IsReached(double time)
    {
        if (time >= timeCap)
        {
            timeCap += originalTimeCap;
            return true;
        }
        return false;
    }

    // Set the cap for this timer
    public void SetCap(double timeCap)
    {
        this.timeCap = timeCap;
    }

}
