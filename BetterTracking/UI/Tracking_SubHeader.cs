﻿#region License
/*The MIT License (MIT)

Better Tracking

Tracking_SubHeader - UI Interface for sub header element

Copyright (C) 2018 DMagic
 
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion

using BetterTracking.Unity.Interface;
using UnityEngine;

namespace BetterTracking
{
    public class Tracking_SubHeader : ISubHeaderItem
    {
        private string _title;
        private int _vesselCount;
        private int _mode;
        private bool _orbitsOn = true;
        private GameObject _headerImage;
        private RectTransform _headerRect;

        private bool _pushedForward;
        private float _cachedZ;

        public delegate void ToggleOrbits(bool orbitOn);

        private ToggleOrbits _onToggleOrbits;

        public Tracking_SubHeader(string title, int vesselCount, GameObject obj, int mode, ToggleOrbits toggle)
        {
            _title = title;
            _vesselCount = vesselCount;
            _headerImage = obj;
            _mode = mode;

            _onToggleOrbits = toggle;

            _headerRect = obj.transform as RectTransform;
        }

        public string HeaderName
        {
            get { return _title; }
        }

        public string HeaderInfo
        {
            get { return _vesselCount.ToString(); }
        }

        public bool OrbitsOn
        {
            get { return _orbitsOn; }
        }

        public GameObject HeaderImage
        {
            get { return _headerImage; }
        }

        public void OnToggleOrbits()
        {
            _orbitsOn = !_orbitsOn;

            _onToggleOrbits(_orbitsOn);
        }

        public void Update(bool dragging)
        {
            if (dragging)
            {
                if (!_pushedForward)
                {
                    _pushedForward = true;

                    _cachedZ = _headerImage.transform.position.z;

                    _headerImage.transform.position = new Vector3(
                        _headerImage.transform.position.x, _headerImage.transform.position.y, _cachedZ - 30);
                }

                if (!_headerImage.activeSelf)
                    _headerImage.SetActive(true);

                return;
            }
            else if (_pushedForward)
            {
                _pushedForward = false;

                _headerImage.transform.position = new Vector3(
                    _headerImage.transform.position.x, _headerImage.transform.position.y, _cachedZ);
            }


            if (_mode > 0 || _headerRect == null || Tracking_Controller.Instance == null)
                return;
        }
    }
}
