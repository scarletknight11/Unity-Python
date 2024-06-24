using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Barracuda;
using UnityEngine;


public class GetInferenceFromModel : MonoBehaviour {


    public Texture2D texture;

    public NNModel modelAsset;

    private Model _runtimeModel;

    private IWorker _engine;

    [Serializable]
    public struct Prediction
    {
        public int predictedValue;
        public float[] predicted;

        public void SetPrediction(Tensor t)
        {
            predicted = t.AsFloats();
            predictedValue = Array.IndexOf(predicted, predicted.Max());
            Debug.Log($"Predicted {predictedValue}");
        }
    }

    public Prediction prediction;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Mathf.Pow(6, 2));
        _runtimeModel = ModelLoader.Load(modelAsset);
        _engine = WorkerFactory.CreateWorker(_runtimeModel, WorkerFactory.Device.CPU);
        prediction = new Prediction();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // making a tensor out of a grayscale texture
            var channelCount = 1; // grayscale, 3 = color, 4 = color+alpha
            var inputX = new Tensor(texture, channelCount);

            Tensor OutputY = _engine.Execute(inputX).PeekOutput();
            inputX.Dispose();
            prediction.SetPrediction(OutputY);
        }
    }

    private void OnDestroy()
    {
        _engine?.Dispose();
    }
}
