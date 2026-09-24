\# VACAC Graduate Software Engineer Task 2026



\## Overview



This project was created for the VACAC Graduate Software Engineer Task 2026.



The project is a modular conveyor building system developed in Unity. The user can select and place different conveyor types, snap conveyor segments together, and create continuous conveyor lines while the simulation is running.



Products are spawned onto the conveyor system and  travel between connected conveyor segments.



\## Features



\- Modular conveyor placement system

\- Conveyor snapping and automatic connections

\- Long and short straight conveyor variants

\- Incline conveyors for moving products upwards

\- Decline conveyors for moving products downwards

\- Runtime conveyor construction

\- Conveyor rotation before placement

\- Multiple product types

\- Random product spawning

\- Products automatically transfer between connected conveyors

\- Products wait at unfinished conveyor ends and continue when a new conveyor is connected

\- Free moving camera controls

\- In game conveyor selection UI, so users can manually select which conveyor to use



\## Controls



\### Conveyor Building



| Input | Action |

| --- | --- |

| 1 | Select Long Conveyor |

| 2 | Select Short Conveyor |

| 3 | Select Incline Conveyor |

| 4 | Select Decline Conveyor |

| Left Mouse Button | Place Conveyor |

| R | Rotate Conveyor |

| Escape | Cancel Placement |



\### Camera



| Input | Action |

| --- | --- |

| W / A / S / D | Move Camera |

| Left Shift | Move Faster |

| Mouse Wheel | Zoom |

| Right Mouse Button + Mouse | Rotate Camera |



\## Development Tools



\- Unity 2022.3.47f1 LTS

\- C#

\- Visual Studio / Visual Studio Code

\- Git

\- GitHub



\## Project Structure



The conveyor system is designed around reusable conveyor segments.



Each conveyor contains input and output connection points. These points are used both for snapping conveyor segments together and for determining the path products follow.



Products use a reusable conveyor movement component and can transition automatically between two connected conveyor segments.



\## Running the Project



\### Windows Build



Run the supplied Windows executable from the release/build folder.



\### Unity Editor



1\. Open the project using Unity 2022.3.47f1 LTS.

2\. Open `MainScene`.

3\. Enter Play Mode.

4\. Use the controls listed above to construct and test conveyor layouts.



\## GitHub Repository



Repository:



\[https://github.com/Faisal49A/VACAC-Graduate-Assessment]


## Demo Video

The demonstration video is included separately in the `Video_Demo` folder in the 'Deliverables'  folder.



\## Notes



The supplied VACAC conveyor and product assets were used to create the conveyor builder environment and product simulation.



The system was developed with modularity in mind so that additional conveyor and product types can be added using the existing conveyor and product components.

