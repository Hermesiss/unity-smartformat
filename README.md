# SmartFormat Unity Extension

## What is this?

A UPM version of [SmartFormat](https://docs.unity3d.com/Packages/com.unity.localization@0.7/manual/SmartStrings.html) for Unity without [com.unity.localization](https://docs.unity3d.com/Packages/com.unity.localization@0.7) dependencies

## Installation

You have several options

### From registry

First, you need to add a scoped registry to `Packages/manifest.json`: 

```json
"scopedRegistries": [
    {
      "name": "trismegistus",
      "url": "http://upm.trismegistus.tech:4873/",
      "scopes": [
        "trismegistus.unity"
      ]
    }
]
```

Then open `Window/Package manager`, `All packages`, and install `Trismegistus SmartFormat`

### From git url

1. Open `Window/Package manager`
2. `+`, `Add from git URL`
3. Enter `https://github.com/Hermesiss/unity-smartformat.git?path=/Packages/trismegistus.unity.smartformat`
