I've been working on this project on/off mostly for my environment design class, but now that's OVER! I passed with a full 100% and a neat environment to boot, I'm really excited about that. As well, I also did some more planning about the lore of my project over a cruise to Mexico my family and I went on. It was all very exciting and a welcome break after another semester of school, work, and an impromptu week of horrid sickness.
In any case, I'm back now with so much drive. I'm really excited because for the first time I feel like I can actually back up being skilled with my game design things. I'm looking for summer jobs so I can work to make some dough and applied very excitedly to a few game programming opportunities. I always doubt myself and undersell my abilities, but it's nice to see that I don't have to all the time here. I've been writing code, recording music, modelling, writing lore and making design decisions, and all that. If that's not game design, then I don't know what is!
I left one thing out, and that's project management. I have a goal behind this project (to make it my senior project) and an actual release. If I graduate in 2024, then I have two years to get this out and all that. Mostly though, it's the workflow and attention to detail that I want to capture about my game and all that.

##  Picking up where I left off
According to my previous note and its heading, [[4-11 to 4-12 Work#Problem 1 Casting the Bobber]], I left off deciding that I wanted a fully-simulated and non-kinematic bobber for casting. This would make the game more fun in my opinion, but I have to make sure I work thoroughly with the physics to make things well (for example, how do I make the bobber *float*, much less right itself in the water and *bob*?).

In any case though, I hadn't gotten much further past the casting stage from there though. The main problem I was encountering was in predicting kinematically where the bobber would land, or simulating that in general. I thought it might be useful to do so so that I might show where the bobber would land.
Then again, Animal Crossing doesn't do that. Zelda games (to my knowledge) didn't do that, Stardew didn't, and more. Granted, fishing wasn't one of the core mechanics of the game... But they didn't display that target location as I recall. I haven't yet looked into other fishing games and all that, but I don't think they did that either.

Maybe I don't need to do that, and I just need to have some *strength meter UI* to show the strength of the cast. Kinda like the horse jumping meter in Minecraft or the Stardew fishing one.

---

The next major changes as I move away from the kinematic system is going to involve a lot of brash, forward action. I'm going to resume work on this tomorrow morning. I'll make tomorrow a day to work on this game.

---

# IT'S TOMORROW NOW BABY
That's right! It's 10am and the first act of sacrilege I'll commit today involves upgrading my project's version from 2020 to 2021. Initially, I wanted an LTS version of Unity guaranteed to work and not come out with a million new versions that I'd need to ugrade to. Turns out, LTS releases still have that too. I figure I might as well migrate to the Unity-recommended version (there must be a reason it's recommended after all). That said, I'm going to try to do this. Let's hope there's no catastrophic changes and breaks that occur, but there's only one way to find out!

Alrighty. So I don't care about the kinematic test points at this point, I only really care about moving forwards. How I see this at the moment is in working on the FishingRod states and the `BobberScript`. My next step, I think, is going to be about getting a valid cast going, and returning the bobber to it's original position if not.
I'm slightly interested in making UI for the casting power and whatnot, but I'll save that for later. It sounds fun but I won't concern myself with that now.

In any case, a cast should only be valid if the bobber hits a surface or collider that we detect as water. If it does, then we have a valid cast. If *not*, then we will pull the bobber back to rod and return to the `FishingRodIdle` state.

#### FishingAreas
I knew I'd have to look at these as well, since the Bobber has to work with these to actually do things. So, I have to decide how I want the fishing area to collide, either as a physical object or as a trigger. I think it makes more sense as a trigger collider, but that then means that to make the bobber "float" I'd have to apply some force to it.

WAIT I like that actually. What if I made tackle to add on, and you could fish for deeper-dwelling fish with weights (like bass IRL)? If that's the case, I'd need some propulsive force to push the bobber up and resist gravity no matter what. How neat!

ALSO, I realize that I can't just have the Bobber sort of float. Truthfully I'm forgtting whether a non-trigger collider can detect a collision with a trigger one. After a test I don't think it did.
Instead, I have to **catch** the Bobber with some force applied from the FishingArea. This means I have to make a `FishingAreaScript` that does this, or something of the like. It feels more like an additional utility script, something like `FloatBobberScript`  or something. They say to avoid monolithic scripts, but I think that since FishingAreas contain fish that will have emergently complex behavior and only really serve to handle this specific interaction, we can use the general name `FishingAreaScript`, that seems apt enough.
	Just had an idea tangentially, about some flow field map thing that stores a direction that you could paint onto some surface, like how you paint on a Terrain? That'd be handy, but I'll have to see how I choose to implement water flow.

I'm thinking, where do I float the bobber? Like actually apply forces to it? I don't feel like doing so in the OnTriggerStay function is really great, it freaks me out. But, it does update on the physics timer... My other approach in mind was to 'register' and store the Bobber that enters the trigger, clearing the data upon exiting. However, different things should be able to float on the water as well...
Whatever, I'm only considering the bobber currently, I'll try to make it float on the surface first and foremost. I'll do it in the OnTriggerStay because I read Unity sample code that employed what I was iffy about.

Now I've got code to catch the bobber but I want it to dampen the velocity and float to the top NICELY and not so bouncily. A way I've done this is applying upwards force to the Bobber (whose x and z velocity values are dampened by drag) that is the Bobber's mass scaled by gravity, plus some additional factor to make it float on the surface.

Quick thing though, is that this solution is the most accurate, and generally what I want, but it takes FOREVER for the bobber to rise to the surface initially. I want to be able to gather analytics about the game eventually too, so I'll add a time spent fishing float that increases as the bobber stays in the FishingArea.
I'll have to store more information about the bobber if I do this though, but this approach allows me to gather analytics and ALSO stamp the time that a bobber entered the FishingArea. Based on the time, I can lerp some upwards force value from a higher one to that target force variable over time when the bobber enters the pool I think.
	There's also the option of doing this conditionally based on the Rigidbody's velocity upon hitting the pool... This approach has a smaller data footprint so I'll try this one first and then settle for the other if I must.
GRRR, it bothers me that I'm continually creating local variables in an updating loop... it's one thing if it's a vector or value type but it's a whole other ordeal in my mind if it's a reference type. Screw it, I'm gathering bobber data and storing it in the fishing area.

I don't want to overthink this, but a registration system opens the door for data collection. I think I want to do it something like this:
- A special class  `FishingAreaSession` will help us maintain the data.
	- If OnTriggerEnter finds a Bobber, we'll "register" it in a new one of these classes and pop it onto a list of active `FishingAreaSessions`.
	- OnTriggerStay will be used to update values like time spent fishing here and things like that.
	- If OnTriggerExit finds a Bobber leaving, we will close the session and pop it onto a list of inactive `FishingAreaSessions`. 
- Two lists for active and inactive `FishingAreaSessions` will maintain these things, and when we exit we can gather information about this.

The main thing about this system is that I didn't want to disallow multiple bobbers, but I also didn't want to be making overly expensive use of local variables and the OnTrigger functions. I think it may be more practical to encapsulate this in another way.

Having implemented a rough version of the system I just described, getting a good "float" is proving to be tough. I have to understand what a good float even means. Currently, I've been trying to just apply a force equal to the bobber's mass multiplied by gravity plus some small force to counter the bobber's weight. Looking at this, I'm merely countering the weight of the bobber in a given gravity. This is neat, but I think I need to do it by the surface of the water and the depth from the top based on the mass.
I'm close with what I'm doing now I think, but I need to get the bobber to just SIT on the surface and not bounce so high that we lose our `FishingAreaSession`. This might help to do VelocityChange force modes instead? I don't know, but I'm going to push my commits for now, OWL is finally back and I want to watch for a moment!