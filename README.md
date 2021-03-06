![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")
# Sitecore Hackathon 2021

- MUST READ: **[Submission requirements](SUBMISSION_REQUIREMENTS.md)**
- [Entry form template](ENTRYFORM.md)
- [Starter kit instructions](STARTERKIT_INSTRUCTIONS.md)
  

### ⟹ [Insert your documentation here](ENTRYFORM.md) <<


# TEAM VR  : BYOD - Object Detection App

## Intro :

As we are now familiar with a lot of AI tools that allow users classify & Tag images but they are mostly limited to a General Dataset - mostly realy world tags like Person, Vehicle, Animal etc. * It takes a huge effort to custom train specific images - let's say images related to a product line **( yes - we have trained to predict Apple Products.)**.
#### So we took this intiative to drive a BYOD pipeline mainly using Sitecore and Headless Service to predict !

## Value add : 
We think this could be of great use as the dataset is created by end users who master their product line and so they can easily build their own ! We then do the difficult part of training it. It can also be coupled with Sitecore.ai and could serve as a tagging service for Content Hub. You can think of so many different use cases !

## Tools Used :
 
 We hava a created **'Build Your Own Dataset'** for Object Detection in Images app using 

  - Sitecore & .net core SDK
  - [Custom Vision Ai](https://www.customvision.ai/)
  - [Vott Tool (Visual Object Tagging Tool)](https://github.com/microsoft/VoTT)
  - Azure Blob Storage - To store images and vott project

## A Brief Workflow :

A short summary on how the whole custom data set training is done.

Upload Image |Tag Image|Export|Train & Predict
:--------------------|:--------------------|:--------------------|:--------------------
Upload image in Custom Folder Within Sitecore  |Tag Image Using VOTT tool which is mounted as external tool and can be accessed from Launchpad|Once images are tagged - click export button that exports to CustomVision.ai|Finally goto Train page - provide a name for your model and click train. Once the model is available, Predictions can be done !

## Docker Setup

- run ./init.ps1 -LicenseXmlPath 'Provide-your-Sitecore-Path'
- run ./Start.ps1 to build and run containers.
- run ./Stop.ps1 to stop containers

## App Setup

App setup is not as easy like the docker - in short you need,

- Azure Blob Storage - to store and process image in VOTT tool
- Custom AI acoount and Api keys

Please follow this document which has clear Setup Instructions

## User Guide

It is quite like the workflow table but please follow this document on how to work on it.

## Reference images

#### 1. Upload in Training Folder
![Upload in Training Folder](/docs/images/1.Upload-Image.png "Upload in Training Folder")

#### 2. Open VOTT tool from LaunchPad
![Open VOTT tool from LaunchPad](/docs/images/2.Open_VOTT.png "Open VOTT tool from LaunchPad")

#### 3. Tag Using VOTT tool
![Tag Using VOTT tool](/docs/images/3.Tag-Images.png "Tag Using VOTT tool")

#### 4. Export Image to Custom Vision
![Export Image](/docs/images/4.Export.png "Export Image")

#### 5. Goto Train page - Train Model
![TrainModel](/docs/images/5.TrainYourModel.png "TrainModel")

#### 6. Predict Images -- (Fun Part :D )
![TrainModel](/docs/images/6.Predictions.png "TrainModel")

