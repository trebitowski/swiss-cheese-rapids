var MobilePlugin = {
     IsMobile: function()
     {
         return (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent));
     },

     UserPrompt: function(str = "Enter a value", currName = "")
     {
        var inputStr = window.prompt(UTF8ToString(str), UTF8ToString(currName));
        // Convert js string to allocated string in C#
        var bufferSize = lengthBytesUTF8(inputStr) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(inputStr, buffer, bufferSize);
        return buffer;
     }
 };
 
 mergeInto(LibraryManager.library, MobilePlugin);