# goedle.io Unity SDK

We now give a brief introduction how easy our plugin for Unity can be implemented. We created a developer friendly SDK and the basic setup just takes about 2 minutes.

---

You can download the SDK via the Unity Asset Store [here](http://u3d.as/RXw).

To get an APP and API key please signup on [goedle.io](http://www.goedle.io).

You can skip the first step if you installed the SDK via the Asset Store.

---

1st step - Add the goedle_io Unity Package as Asset ([download](http://www.goedle.io/unity/goedle_io_sdk.unitypackage)).

![Add the goedle.io Unity Packages manual](http://www.goedle.io/unity/1-goedle_add_asset.png "Add the goedle.io Unity Packages manual")


2nd step - Add “Goedle Analytics” as component. You simply type in `Goedle`, Unity finds the rest. 

![Add goedle.io SDK as component](http://www.goedle.io/unity/2-goedle_add_component.png "Add goedle.io SDK as component")


3rd and last step - You have to fill your credentials, APP and API key in.

![Add App and API key](http://www.goedle.io/unity/3-goedle_credentials.png "Add APP and API key")


Afterward, you are ready to start and add the tracking points all over neuralgic positions in your Unity app.

#Tracking Concept

We typically use two main variables to track an event. The first is the action, e.g., view, share, like these variables are the specific actions which are done by a user. The second variable further specifies the action e.g., product, intro, category, user. These two variables are concatenated and delimited with a `.`. Note however that a specifier is not mandatory and we also support hierarchies with more than two levels.

Furthermore, we offer a custom field `event_id`.

As an example, if you would want to track the view-event of a product, the event would be `view`, the specifier would be `product` and the event id `235123`

If your event has a certain value, like a duration or a reached player level, we provide the custom field `event_value`.

The event name would be `finish.level`, the event id could be `level-12` and the event value something like `65`.

Where 65 would be the duration in seconds.


##Game State

If you want to track a game state. We need additional preparation of your code.
You need two global variables
```
    // Game state config
    private float nextActionTime = 0.0f;
    // 15.0f means a period of 15 seconds, this is complete sufficent to track a game state.
    public float period = 15.0f;
```
We have to add the periodical call to the `Update()` function.

```
void Update () {
// Your code ...
        if (Time.time > nextActionTime ) {
            nextActionTime += period;
            GoedleAnalytics.track ("game.state", "stamina", "15");
            GoedleAnalytics.track ("game.state", "mana", "40");
            GoedleAnalytics.track ("game.state", "level", "5");
            GoedleAnalytics.track ("game.state", "pvp", "true");
            GoedleAnalytics.track ("game.state", "joker", "78");
        }
}
```

Please name the event `game.state`, so it can be identified as such.

#Tracking Code

Add to the header of your class file 

```
using goedle_sdk;
```
---
We have three different tracking signatures:

##Event Tracking

Tracking a single event.

```
GoedleAnalytics.track ("start.game");
```


Tracking a single event with an identifier.

```
GoedleAnalytics.track ("reached.level", "level-2");
```


Tracking a single event with an identifier and a value.

```
GoedleAnalytics.track ("finish.round", "round_12", "65");
```

##User information

###User Id

```
GoedleAnalytics.setUserId ("unique_user_id");
```

If you don't set a user id, we create one for you. But, the automatically created user ids are only session based. This is done, because of privacy reasons.

### Traits

!!!Information For now, we only support "first_name" and "last_name" as a trait and additional user information.

```
GoedleAnalytics.trackTraits ("first_name", playerName);
```

```
GoedleAnalytics.trackTraits ("last_name", playerName);
```


### Group

```
GoedleAnalytics.track ("group", "guild", playerGuildName);
```