# jumper-assignment-Timothy-N

![image](https://github.com/AP-IT-GH/jumper-assignment-Timothy-N/assets/84501282/e8e21aa8-59d5-41af-8d36-2f432d26b13e)

- Set-up: Een agent die obstakel ontwijkt en coins pakt. Scene bevat agent, coin,
  en obstakels.
- Goal: een agent die punten krijgt door obstakels te ontwijken en coins te pakken
- Agents: de omgeving heeft 1 agent.
- Agent Reward Function:
  - raakt de coin +1
  - raakt het obstakel -1
- Behavior Parameters:
  - Visual Observations: kijkt naar de opkomende objecten
  - kijkt of hij de vloer raakt

# tutorial

- stap1: omgeving maken
  - maak een empty in je scene met de naam "playfield"
  - maak in die empty playfield een plane en geef hem de tag "floor"
  - scale de plane met x=5
  - maak een cube dan ook in de playfield
  - plaats die achter de plane om een muur te maken
  - maak materials om de playfield kleuren te geven

- stap 2: agent maken
  - maak een cube aan genaamd "agent"
  - plaats het in het midden op de plane
  - geef de cube een rigidbody
  - freeze al de posities behalve de y en freeze al de rotations
  - geef het de script Behavior parameters
  - geef het de script Ray Perception Sensor 3D
- stap 3: enemy maken
  - maak een cube aan genaamd "enemy"
  - geef de cube een rigidbody en haal gravity weg
  - geef de tag "Enemy"
  - maak er een prefab van en haal het weg van de scene
- stap 4: coin maken
  - maak een sphere aan genaamd "coin"
  - geef de cube een rigidbody en haal de graviry weg
  - freeze al de rotations
  - geef de tag "Coin"
  - maak er een prefab van en haal het van de scene
- stap 5: yaml file configureren
  - maak een map aan in de assets folder met de naam "config"
  - plaats hier de file jumper.yaml
  - plaats het volgende in de jumper.yaml:
  - behaviors:
    ```
     My Behavior:
      trainer_type: ppo
      hyperparameters:
        batch_size: 128
        buffer_size: 2048
        learning_rate: 0.0003
        beta: 0.005
        epsilon: 0.2
        lambd: 0.9
        num_epoch: 5
        learning_rate_schedule: linear
        beta_schedule: constant
        epsilon_schedule: linear
      network_settings:
        normalize: false
        hidden_units: 128
        num_layers: 2
      reward_signals:
        extrinsic:
          gamma: 0.90
          strength: 1.0
      max_steps: 15000000
      time_horizon: 64
      summary_freq: 2000
    ```
- stap 6: script schrijven
  - parameters die je moet krijgen zijn de coin prefab en enemy prefab
  - bij elke episode begin laat je een random nummer tussen 1-2 kiezen welke prefab hij laat spawnen
  - die prefab moet een force krijgen richting de agent
  - voor observaties moet hij de local positie hebben
  - bij elke collision moet je kijken met welke tag en dan je reward bepalen
  - als het met de tag floor is moet je een boolean veranderen om het te kunnen laten springen
  - geef de script aan de agent
  - geef de juiste parameters aan het script
- stap 7: start de ml agent met anaconda
  - ga naar de project directory en in de assets
  - gebruik deze comando mlagents-learn config/jumper.yaml --run-id=CubeAgent
  - ga naar je unity project en start het

# test

het testen ging heel moeilijk ik heb moeite met mijn agent te laten springen hierdoor is de opdracht niet echt gelukt. 



 

