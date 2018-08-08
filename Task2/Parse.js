function parse(template, text) {
            var result = [];
            var tags = [];
            var values = [];
            var rex = /\[\{[A-Z]*[0-9]*\}\]/g;

            tags = template.match(rex);
            var temp = template.split(rex);
            
            while (temp.length > 0) {
                var before = temp.shift();
                var index = text.indexOf(before);
                if (index > 0) {
                    values.push(text.substring(0, index));
                    text = text.replace(values[values.length - 1], "");
                }
                text = text.replace(before, "");  
            };

            if (text.length > 0) values.push(text);            

            var i = 0;
            tags.forEach(function (item) {
                result.push({
                    Tag: tags[i],
                    Value: values[i]
                });
                i++;
            });

            return result;
        };