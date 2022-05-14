{
  "Author": "AnyCAD",
  "Name": "ABB6700_ZF 6R",
  "Description": "ABB6700_ZF 6R..",
  "ModelPath": ".",
  "ModelType": "BREP",
  "Arms": [
    {
      "Name": "",
      "Joints": [
		{
		  "Name": "Base",
		  "Type": "Fixed",
		  "Model": "jizuo.brep",
		  "DH": {
			"Alpha": 0,
			"A": 0,
			"D": 0,
			"Theta": 0
		  },
		  "BiasDH": null
		},
		{
		  "Name": "AXIS1",
		  "Type": "Revolute",
		  "Model": "AXIS1.brep",
		  "DH": {
			"Alpha": 0,
			"A": 0,
			"D": 780,
			"Theta": 0
		  },
		  "BiasDH": null
		},
		{
		  "Name": "AXIS2",
		  "Type": "Revolute",
		  "Model": "AXIS2.brep",
		  "DH": {
			"Alpha": -90,
			"A": 320,
			"D": 0,
			"Theta": -90
		  },
		  "BiasDH": null
		},
		{
		  "Name": "AXIS3",
		  "Type": "Revolute",
		  "Model": "AXIS3.brep",
		  "DH": {
			"Alpha": 0,
			"A": 1125,
			"D": 0,
			"Theta": 0
		  },
		  "BiasDH": null
		},
		{
		  "Name": "AXIS4",
		  "Type": "Revolute",
		  "Model": "AXIS4.brep",
		  "DH": {
			"Alpha": -90,  
			"A":200,
			"D": 1142.5,
			"Theta": 0
		  },
		  "BiasDH": null
		},
		{
		  "Name": "AXIS5",
		  "Type": "Revolute",
		  "Model": "AXIS5.brep",
		  "DH": {
			"Alpha": 90,
			"A": 0,
			"D": 0,
			"Theta": 0
		  },
		  "BiasDH": null
		}
		,
		{
		  "Name": "AXIS6",
		  "Type": "Revolute",
		  "Model": "AXIS6.brep",
		  "DH": {
			"Alpha": -90,
			"A": 0,
			"D": 1000,
			"Theta": 180
		  },
		  "BiasDH": null
		}
      ]
    }
  ],
  "Transform": {
    "Location": [
      0,
      0,
      0
    ],
    "Angle": 0,
    "Scale": 0
  },
  "AxisSize": 100
}