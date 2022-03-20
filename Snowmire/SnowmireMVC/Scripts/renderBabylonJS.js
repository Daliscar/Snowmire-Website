function RenderBabylonJs() {
    const canvas = document.getElementById("renderCanvas"); // Get the canvas element
    const engine = new BABYLON.Engine(canvas, true, null, true); // Generate the BABYLON 3D engine

    // Add your code here matching the playground format
    var createScene = function () {

        //#region MESHES JSON
        var meshesTEXT = '{ "meshNumber" : [' +
            '{"name" : "house_Cube", "path" : "https://snowmireawscdn.s3.eu-central-1.amazonaws.com/", "fileName" : "Dark-Knight2.obj"},' + //best mesh is Dark-Knight2.obj - https://raw.githack.com/
            '{"name" : "Cube", "path" : "https://cdn.jsdelivr.net/gh/Daliscar/3DModels/", "fileName" : "testCubeforSite.babylon"},' +
            '{"name" : "house_Cube", "path" : "https://cdn.jsdelivr.net/gh/Daliscar/3DModels/", "fileName" : "Dark-Knight2.obj"},' +
            '{"name" : "Cube", "path" : "https://cdn.jsdelivr.net/gh/Daliscar/3DModels/", "fileName" : "testCubeforSite.babylon"},' +
            '{"name" : "house_Cube", "path" : "https://cdn.jsdelivr.net/gh/Daliscar/3DModels/", "fileName" : "Dark-Knight2.obj"}' +
            ']}';
        const meshesJSON = JSON.parse(meshesTEXT);
        //#endregion

        //#region SCENE SETUP
        var scene = new BABYLON.Scene(engine);
        // This creates and positions a free camera (non-mesh)
        var camera = new BABYLON.ArcRotateCamera("Camera", 0, 1, 7, new BABYLON.Vector3(0, 0, 0), scene);

        // This targets the camera to scene origin
        camera.setTarget(new BABYLON.Vector3(0, 2, 0));
        var ground = BABYLON.MeshBuilder.CreateGround("ground", { width: 6, height: 6 }, scene);

        // This attaches the camera to the canvas
        scene.registerBeforeRender(function () {
            camera.alpha += 0.005;
            ground.rotation.y -= 0.005;
        });
        camera.beta = 1.5;

        // This creates a light, aiming 0,1,0 - to the sky (non-mesh)
        var light = new BABYLON.HemisphericLight("light", new BABYLON.Vector3(0, 1, 0), scene);

        // Default intensity is 1. Let's dim the light a small amount
        light.intensity = 2;
        //#endregion

        var light = new BABYLON.HemisphericLight("light", new BABYLON.Vector3(1, 1, 0));

        //#region REMOVE MODEL
        var clearMeshes = function () {
            scene.meshes.forEach((mesh, index) => {
                setTimeout(() => {
                    mesh.dispose();
                })
            });
        }
        //#endregion

        var currentPage = 1;
        //#region LOAD MODEL
        var loadMeshes = function (action) {

            switch (action) {
                case "next":
                    if (currentPage >= meshesJSON.meshNumber.length) {
                        currentPage = 1;
                        text1.text = currentPage + "/" + meshesJSON.meshNumber.length;
                    }
                    else {
                        currentPage++;
                        text1.text = currentPage + "/" + meshesJSON.meshNumber.length;
                    }
                    var ground = BABYLON.MeshBuilder.CreateGround("ground", { width: 6, height: 6 }, scene);
                    BABYLON.SceneLoader.ImportMesh(
                        "",
                        meshesJSON.meshNumber[currentPage - 1].path,
                        meshesJSON.meshNumber[currentPage - 1].fileName,
                        scene,
                        function () {
                        });
                    console.log(currentPage);
                    break;


                case "previous":
                    if (currentPage > 1) {
                        currentPage--;
                        text1.text = currentPage + "/" + meshesJSON.meshNumber.length;
                    }
                    else {
                        currentPage = 1;
                        text1.text = currentPage + "/" + meshesJSON.meshNumber.length;
                    }
                    var ground = BABYLON.MeshBuilder.CreateGround("ground", { width: 6, height: 6 }, scene);
                    BABYLON.SceneLoader.ImportMesh(
                        "",
                        meshesJSON.meshNumber[currentPage - 1].path,
                        meshesJSON.meshNumber[currentPage - 1].fileName,
                        scene,
                        function () {
                        });

                    console.log(currentPage);
                    break;
                default: console.log('Avoid big big crash <3');
            }



        };
        //#endregion

        BABYLON.DefaultLoadingScreen.prototype.displayLoadingUI = function () {
            if (document.getElementById("customLoadingScreenDiv")) {
                // Do not add a loading screen if there is already one
                document.getElementById("customLoadingScreenDiv").style.display = "initial";
                return;
            }
            this._loadingDiv = document.createElement("div");
            this._loadingDiv.id = "customLoadingScreenDiv";
            this._loadingDiv.innerHTML = "LOADING";
            var customLoadingScreenCss = document.createElement('style');
            customLoadingScreenCss.type = 'text/css';
            customLoadingScreenCss.innerHTML = `
    #customLoadingScreenDiv{
        background: #000000;
        color: white;
        font-size:50px;
        text-align:center;
        border-radius:5%;
    }
    `;
            document.getElementsByTagName('head')[0].appendChild(customLoadingScreenCss);
            this._resizeLoadingUI();
            document.addEventListener("readystatechange", this._resizeLoadingUI);
            window.addEventListener("resize", this._resizeLoadingUI);
            document.body.appendChild(this._loadingDiv);
        };

        BABYLON.DefaultLoadingScreen.prototype.hideLoadingUI = function () {
            document.getElementById("customLoadingScreenDiv").style.display = "none";
            console.log("scene is now loaded");
        }

        //#region BUTTONS
        //ADDING UI
        var UI = new BABYLON.GUI.AdvancedDynamicTexture.CreateFullscreenUI("UI");

        var URL = "https://snowmireawscdn.s3.eu-central-1.amazonaws.com/background.png";

        var border = new BABYLON.GUI.Image("backImage", URL);
        border.width = 1;
        border.height = 1;
        UI.addControl(border);

        var button1 = BABYLON.GUI.Button.CreateSimpleButton("but1", ">");
        button1.left = "-4%";
        button1.width = "40px"
        button1.height = "40px";
        button1.color = "white";
        button1.cornerRadius = 20;
        button1.background = "blue";
        button1.onPointerUpObservable.add(function () {
            engine.displayLoadingUI();
            clearMeshes();
            loadMeshes("next");
        });
        UI.addControl(button1);
        var button2 = BABYLON.GUI.Button.CreateSimpleButton("but2", "<");
        button2.left = "4%";
        button2.width = "40px"
        button2.height = "40px";
        button2.color = "white";
        button2.cornerRadius = 20;
        button2.background = "blue";
        button2.onPointerUpObservable.add(function () {
            engine.displayLoadingUI();
            clearMeshes();
            loadMeshes("previous");
        });
        UI.addControl(button2);

        var text1 = new BABYLON.GUI.TextBlock();
        text1.text = currentPage + "/" + meshesJSON.meshNumber.length;
        text1.width = 0.2;
        text1.height = "40px";
        text1.color = "white";
        text1.verticalAlignment = BABYLON.GUI.Control.VERTICAL_ALIGNMENT_TOP;
        text1.horizontalAlignment = BABYLON.GUI.Control.HORIZONTAL_ALIGNMENT_RIGHT;
        UI.addControl(text1);

        button1.horizontalAlignment = BABYLON.GUI.Control.HORIZONTAL_ALIGNMENT_RIGHT;
        button2.horizontalAlignment = BABYLON.GUI.Control.HORIZONTAL_ALIGNMENT_LEFT;

        // Initialize Index #1 view
        var init = function () {
            engine.displayLoadingUI();

            BABYLON.SceneLoader.ImportMesh(
                "",
                meshesJSON.meshNumber[0].path,
                meshesJSON.meshNumber[0].fileName,
                scene,
                function () {
                });
        }

        init();

        scene.onDataLoadedObservable.add(() => {
            engine.hideLoadingUI();
        })


        //#endregion
        return scene;
    };

    var scene = createScene(); //Call the createScene function
    var container = new BABYLON.AssetContainer(scene);
    // Register a render loop to repeatedly render the scene

    engine.runRenderLoop(function () {
        scene.render();
    });
    setTimeout(console.log.bind(console, "%cNO TOUCHY!😡", "margin:10%;font-size:50px;line-height: 60px;"));
}