/*
 * Copyright (c) 2011 Jack Senechal. All rights reserved.  Use of this
 * source code is governed by a BSD-style license that can be found in the
 * LICENSE file.
 */

/*
 *  Modified by Vincent Qiu.
 *  Chinese telephone number detector and #AJAX with DirectHR Softphone Agent(not completed)
 */
function getDirectDial() {
    return function (info, tab) {

        // The srcUrl property is only available for image elements.
        var url = 'http://127.0.0.1:7069/Dial/' + info.selectionText;
        // Create a new window to the info page.
        chrome.windows.create({ url: url, width: 520, height: 660 });
    };
};

function getDirectDialwithZero() {
    return function (info, tab) {

        // The srcUrl property is only available for image elements.
        var url = 'http://127.0.0.1:7069/Dial/0' + info.selectionText;
        // Create a new window to the info page.
        chrome.windows.create({ url: url, width: 520, height: 660 });
    };
};

function getPreDial() {
    return function (info, tab) {

        // The srcUrl property is only available for image elements.
        var url = 'http://127.0.0.1:7069/PreDial/' + info.selectionText;
        // Create a new window to the info page.
        chrome.windows.create({ url: url, width: 520, height: 660 });
    };
};

/**
* Create a context menu which will only show up for images.
*/
chrome.contextMenus.create({
    "title": "Dial %s",
    "type": "normal",
    "contexts": ["selection"],
    "onclick": getDirectDial()
});

chrome.contextMenus.create({
    "title": "Dial 0%s",
    "type": "normal",
    "contexts": ["selection"],
    "onclick": getDirectDialwithZero()
});

chrome.contextMenus.create({
    "title": "Edit before dial %s",
    "type": "normal",
    "contexts": ["selection"],
    "onclick": getPreDial()
});


var defaults = {
    intlRegex: /([\+]{0,1}86[0-9. -]{10,18})|([0-9]{2,4}[. -]{0,1}[0-9]{7,8})|(400|800)([2-9][0-9]{6,11})|(1[3-9][0-9]{9})|([0-9]{7,8})/,
    intlReplacement: '$1$2$3$4$5$6',
    //  homeRegex: /(?:\b|(?:((?:\b|\+)1)[. -]?)|\()(\d{3})\)?[. -]?(\d{3})[. -]?(\d{4})(?:\b|x\d+)/,
    //  homeReplacement: '$1$2$3$4',
    linkType: 'http://127.0.0.1:7069/PreDial/'
}

//function loadOptions() {
//  // set up link type
//  var linkType = localStorage['linkType'] || defaults['linkType'];
//  var linkTypeSelect = document.getElementById('linkType');
//  for (var i = 0; i < linkTypeSelect.children.length; i++) {
//    var child = linkTypeSelect.children[i];
//    if (child.value == linkType) {
//      child.selected = 'true';
//      break;
//    }
//}
//  // localStorage['intlRegex'] || 
//  // set up international number regex
//  var intlRegex =defaults['intlRegex'];
//  document.getElementById('intlRegex').value = intlRegex.toString();
//  // set up home number regex
//  var homeRegex = localStorage['homeRegex'] || defaults['homeRegex'];
//  document.getElementById('homeRegex').value = homeRegex.toString();

//   //set up international number replacement localStorage['intlReplacement'] || 
//  var intlReplacement =defaults['intlReplacement'];
//  document.getElementById('intlReplacement').value = intlReplacement.toString();
//  // set up home number replacement
//  var homeReplacement = localStorage['homeReplacement'] || defaults['homeReplacement'];
//  document.getElementById('homeReplacement').value = homeReplacement.toString();
//}

//function saveOptions() {
//  try {
//    localStorage['linkType'] = document.getElementById('linkType').value;
//    localStorage['intlRegex'] = RegExp(document.getElementById('intlRegex').value.replace(/^\/|\/$/g, ""));
//    localStorage['homeRegex'] = RegExp(document.getElementById('homeRegex').value.replace(/^\/|\/$/g, ""));
//    localStorage['intlReplacement'] = document.getElementById('intlReplacement').value;
//    localStorage['homeReplacement'] = document.getElementById('homeReplacement').value;
//    setStatus('Options Saved.');
//  } catch (error) {
//    alert(error);
//  }
//}

function setStatus(message) {
  var status = document.getElementById('status');
  status.innerHTML = message;
  setTimeout(function() {
    status.innerHTML = '';
  }, 4000);
}

//function clearData() {
//  if (confirm('Clear data in extension? (includes extension settings)')) {
//    localStorage.clear();
//    alert('Extension data cleared.');
//  }
//}

