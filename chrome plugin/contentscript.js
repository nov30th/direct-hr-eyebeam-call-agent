/*
 * Copyright (c) 2011 Jack Senechal. All rights reserved.  Use of this
 * source code is governed by a BSD-style license that can be found in the
 * LICENSE file.
 */
/*
* Copyright (c) 2011 Vincent Qiu. All rights reserved.  Use of this
* source code is governed by a BSD-style license that can be found in the
* LICENSE file.
* Modified the link style for Agent software.
*/
// load options
// Copyright (c) 2011 The Chromium Authors. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.





chrome.extension.sendRequest({ action: 'options' }, function (response) {
    console.log('send Request working here...');
    // the localStorage mechanism converts the regex to a string, so we have to convert it back
    var stripper = /^\/|\/$/g;
    var intlRegex = RegExp(response.options.intlRegex.replace(stripper, ''), 'gm');
    //  var homeRegex = RegExp(response.options.homeRegex.replace(stripper, ''), 'gm');

    var linkType = response.options.linkType;
    var intlReplacement = '<a href="' + linkType + response.options.intlReplacement + "src=" + escape(window.location) + '" target="_black">$&</a>';
    //  var homeReplacement = '<a href="' + linkType + response.options.homeReplacement + '" target="_black">$&</a>';

    var found = false;

    // Test the text of the body element against our international regular expression.
    if (window.location.href.indexOf("http://ehire.51job.com/Candidate/SearchResume.aspx")>=0){
        console.log('51job search page, plugin was disabled.');
        return;
    }
    var directhr_softphone_meta = $('meta[name=directhr_telephone_plugin]').attr("content");
    if (directhr_softphone_meta != undefined && directhr_softphone_meta.toLowerCase().indexOf("[disabled]") >= 0) {
        console.log('directhr meta was found, disabled value was found, plugin on this page was disabled');
        return;
    }
    if (intlRegex.test(document.body.innerText)) {
        $(document).find(':not(textarea)').replaceText(intlRegex, intlReplacement);
        found = true;
    }
    // Test the text of the body element against our home regular expression.
    //  if (homeRegex.test(document.body.innerText)) {
    //    $(document).find(':not(textarea)').replaceText( homeRegex, homeReplacement );
    //    found = true;
    //  }
    if (found) {
        // Notify the background page to update the page icon
        chrome.extension.sendRequest({ action: 'showPageAction' }, function () { });
    }
});