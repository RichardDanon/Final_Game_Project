# Final_Game_Project

<br/>
<p align="center">
  <h3 align="center">Mini Golf Game</h3>

  <p align="center">
    This is a multiplayer golf game so grab friends !
    <br/>
    <br/>
  </p>
</p>

![Contributors](https://img.shields.io/github/contributors/DylanBrass/Final_Game_Project?color=dark-green) ![Forks](https://img.shields.io/github/forks/DylanBrass/Final_Game_Project?style=social) 

## Table Of Contents

* [About the Project](#about-the-project)
* [Built With](#built-with)
* [Getting Started](#getting-started)
  * [Prerequisites](#prerequisites)
  * [Installation](#installation)
* [Usage](#usage)
* [Authors](#authors)
* [Demo](#demo)
* [What to Change](#what-to-change)


## About The Project

![Screen Shot](images/screenshot.png)

This project was made with 3 other teammates, we each had our specialties and I was in charge of coding the multiplayer aspects. This project was our final project to show what we learned during or "Gaming with unity" class. 

These were the requirements : 

![image](https://github.com/DylanBrass/Final_Game_Project/assets/71225455/486eb4d2-5dd1-414c-b193-31c684e31725)

We also had to present this project to the class and explain our code and what we have done. 

## Built With

- Used Unity

- Used Unity Multiplayer

- Used C#

## Getting Started

To get a local copy up and running follow these simple example steps.

### Prerequisites

- First you need to have unity installed, we used version 2022.2.2f1, to avoid any problems I recommend using the same.


### Installation

- Ounce Unity is installed clone the repo with :

```sh
  git clone https://github.com/DylanBrass/Final_Game_Project.git
```

- Then open Unity
- Open this menu :

![image](https://github.com/DylanBrass/Final_Game_Project/assets/71225455/9443d734-e608-4030-b5f4-9bce0ebb364d)

- Click "Add Project From Disk"
- Select the cloned project folder :

![image](https://github.com/DylanBrass/Final_Game_Project/assets/71225455/017162d5-b933-4113-a45d-b42ad12218fe)

Then press "Add Project"

- Finally open it !

![image](https://github.com/DylanBrass/Final_Game_Project/assets/71225455/2adf9688-d081-4dd7-b5a5-aa1bfa76813f)



## Usage

- To use the game to play with a friend you need to build it
- To build you simply do CTRL + b
- When prompted to select a folder choose an empty one (create one)
- The game will then open !

### For Multiplayer
- Simply follow the same steps on another computer
- This game works only with LAN (We do not have dedicated servers), this means both PC's must be on the same WIFI
- One player clicks host
- The host sends the IP to the other player to copy (there's a copy button, it will lag your game)
- The Second player (or more) puts the IP in here :

![image](https://github.com/DylanBrass/Final_Game_Project/assets/71225455/47526bed-3b25-4971-95e0-db80f4f611e7)

- Then click join and voila !


## Demo


https://github.com/DylanBrass/Final_Game_Project/assets/71225455/380d52a1-2edb-4d0d-b225-11fc4dfc575e

## Interesting code 

```csharp
private void Start()
{

    rb2d = gameObject.GetComponent<Rigidbody2D>();

    audioSource = gameObject.AddComponent<AudioSource>();
    hitSound = Resources.Load<AudioClip>("Hit");
    audioSource.clip = hitSound;

    if (IsLocalPlayer)
    {
        lineRenderer = this.gameObject.AddComponent<LineRenderer>();

        //This does not work in the build version: I am guessing it's because it 
        //does not have acess to the Shaders library
        //lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));

        lineRenderer.material = new Material(lineMaterial);
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = new Color(255, 2255, 255, 0.25f);
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.renderingLayerMask = 3;
    }


}

//put as update
private void Update()
{

    if (IsLocalPlayer)
    {
        if (this.gameObject != null && Camera.main != null)
        {
            //determine direction of hit
            Vector2 directionOfHit = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.gameObject.transform.position;

            float strengthOfHit = Mathf.Clamp(Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)), 0, maxDragLength);
            Vector2 endPos = (Vector2)transform.position + (directionOfHit.normalized * strengthOfHit);
            //check if ball is moving so ball cant be hit while moving
            if (rb2d.velocity.magnitude < 0.05f)
            {
                isMoving = false;
                gameObject.GetComponent<Collider2D>().isTrigger = true;
            }
            else
            {
                isMoving = true;
                gameObject.GetComponent<Collider2D>().isTrigger = false;
            }


            if (!isMoving)
            {
                //draw line for how strong ball is hit

                lineRenderer.SetPosition(1, endPos);


                lineRenderer.SetPosition(0, this.gameObject.transform.position);


                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    //calculate force of hit
                    float force = (Vector2.Distance(this.gameObject.transform.position, endPos) * 100 / maxDragLength);
                    //add force to the direction determined higher up
                    rb2d.AddForce(6.5f * force * -(endPos - (Vector2)transform.position).normalized);
                    GlobalVariables.numOfHitsForLvl++;
                    audioSource.Play();
                }
            }



        }
    }
    if (isMoving)
        gameObject.GetComponent<Collider2D>().isTrigger = false;

}
```

This code is what make the line behing the ball appear and determine the strength of the hit. I really liked thiis code for how simple it ended up being, we used line renderer  to draw the line to the cursor with a maximum length. Then with a calculation here :
```cs
 rb2d.AddForce(6.5f * force * -(endPos - (Vector2)transform.position).normalized);
```
We determine the hit strength based off how far the cursor is from the ball.


## What to change

As you can see there is a few bugs and polish problems, these are simply because we ran out of time since we were in our 4 semester with 7 classes and we needed to do other projects. We would all like to fix those. Otherwise more levels and cleaner multiplayer interactions are also on the list to do.

## Authors

* **DylanBrass** - *Comp Sci Student* - [DylanBrass](https://github.com/DylanBrass/) - *Coded the Multiplayer and gameplay features*
* **RichardDanon**  - *Comp Sci Student* - [RichardDanon](https://github.com/RichardDanon/) - *Made UI and graphics as well as level 1 and 4*
* **emilegirardGit**  - *Comp Sci Student* - [emilegirardGit](https://github.com/emilegirardGit/) - *Made level 3 and sand, some animation work*
* **MinseokBUZZ**  - *Comp Sci Student* - [MinseokBUZZ](https://github.com/MinseokBUZZ/) - *Made level 2*

## Acknowledgments 

- [Flexible Color Picker ](https://assetstore.unity.com/packages/tools/gui/flexible-color-picker-150497)
