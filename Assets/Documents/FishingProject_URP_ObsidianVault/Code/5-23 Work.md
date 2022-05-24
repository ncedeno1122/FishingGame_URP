# OH YEAH  Baby
It's 9am, I got up nice and early and I'm ready to RUMBLE! I made a little coffee but am saving breakfast for a bit later, and I'm PUMPED! What a great weekend I had hanging out with my family and friends (not to mention the delicious food I had as well). Lucky me. In any case, let's resume with some joint work!

## Where I Left Off ([[5-13 Work]])
In the last addition to this project, I learned that Rigidbodies shouldn't be dynamically parented and unparented. It really sucked because I wasn't sure how I might attach one Rigidbody to another UNTIL I remembered about Joints. Having done a moment more's research on them, it turns out that those Joints are THE way to do what I'm looking for, I just have to see how exactly I might implement them here.

Hoohoo, yeah boy. These Joints are sounding mighty tasty, they're exactly what I need to solve this problem. The problem lies now in finding what exact Joint behavior I might need. What's more, there's something else I've been thinking of that these joints might allow me to address.
	I remember watching a cool video of a simulated wire thing for a VR game where a physics-based plug and socket was implemented for a puzzle game. It was cool because the cord both behaved how we expected (thanks to joints) AND limited the maximum pull-length of the cord.
	I **could** try to do that here with the line for my fishing rod. It bothered me before how I might avoid problems like walking away from the line and the bobber not getting pulled as we expect it and not seeing a fishing line when we cast. This might be an opportunity!

So I'm THINKING that I might need to use Configurable Joints (just so that I can chain them together for delicious usage) along with a Fixed Joint that I can use to "parent" the bobber again. I figure that if I make Configurable Joints that join the fishing line, I can impose a linear limit on them to have that pull-at-max-distance effect.

My only thing is, I'm not sure if the actual joints between the Rigidbodies can collide with things? I don't think so, since I'm having colliders explicitly there. Getting a line untangled in real life is hard enough LOL, but I can't imagine it being any more fun in Unity.

In any case, things seem like they might work, I just have to change my fishing states to reflect some of these functionalities.

Firstly, though, I have to restructure my fishing rod prefab. As it is now, I have my parent transform that contains the mesh for the fishing rod, which will then need to have all of the fishing line Rigidbodies attached, finally attached to the Bobber. I'll keep the bobber origin transform with with the rod mesh. That should probably work.

---

Guh, before I've gotten to the good stuff I'm starting to overthink things.
Basically, I want to have my whole Rigidbody system the way it is. I wanted to organize it via parenting, but I remembered that parenting with Rigidbodies SUCKS. It is organized nicely when I DO parent it, though, which is annoying.

First off, though, it seems that Joints work by creating a relationship FROM one rigidbody TO another. So I want to be thinking upwards, I guess. My main thing is that I want to organize my fishing rod things somehow in the hierarchy, but I'm not sure how I might do that now... My thing is that it would really be nice to organize them nicely.

Rrgh, this is hurting my head and I'm getting hungry. What I'm encountering is that it's either going to take a very thorough understanding of joints and such to make this properly work, OR I can just simulate the bobber.

Either way, at the VERY LEAST I know that I want a fixed joint to do the attaching and detaching thing. It's just finding the OTHER appropriate relationship between Rigibodies that's getting me.

OR, I could script my own joint... But that might be kinda crazy. If I don't understand how the joints work on their own, it might be hard AND redundant to make my own. Though, I'm not really looking for anything that's significantly complex.

---

I think I might have it! Now to come up with a scripting solution that can help with this...

So I learned how to work with the Configurable Joint and its linear limit, which is what I was looking for all along. NOW, I have to find some way to manage the settings of these via a script. As well, likely to use a line renderer to control the whole thing.

I now just have to think about how I want this to influence the Bobber and all that. I have to create some rather large space allowance for the Bobber on the linear limit IF the links **actually** influence it. I think it probably should.

That will likely involve me changing the linear limit for these things when the Bobber is active vs inactive. I also have to ensure that the Bobber is kinematic when it's not being cast. That way I can secure a fixed joint to the base rigidbody, become kinematic, and attach via the fixed joint to do what I want. I **think** I might have it now.

I'm just trying to think about the process of reeling the bobber back. That probably looks like me reducing the distance of the linear limits of all involved configurable joints by some small amount. That way, the line naturally pulls back.
	But what about a fish pulling on the line? It obviously tries to apply some force to the rope. Maybe we break it?
	It's quite clear that I really need to think about the spring forces of the rope as I go with this.

Whatever, I'm going to just try to get some of this working. But maybe after a snack, I'm getting hungry now. When I get back, I'm going to try to make a script that maintains the fishing line (maybe a little line renderer action could work too!). I want to be able to reel the line in or set the linear limits of all joints in the fishing line, and I want to be able to get the distance the bobber is from the base point and all that.

---
I worked a bit more today on mocking up a physics test scene. There, I figured out more of the structure that will likely work for me, but I wasn't able to find a way to organize this better in the hierarchy yet. I'm not sure if there's a way to do this, but I'm asking around in a few game-dev servers as all of my internet searches and testing is telling me there's  not.

I'm just a bit worried about how I'd organize these assets if I needed to load them or something, but I think I could just assemble them like a chad and link them together upon actually loading the asset. Not to mention, I don't know if I'd ever need to unload the fishing rod...

In any case, that's all the work I did today.