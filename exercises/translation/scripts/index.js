let showFirst,loadFormer;
let vocabulary;
const vocabularies={
    "mid_vocab": "中考考纲词汇/词组"
};

($(()=>{
    getVocabularies();

    $("#start").click(()=>{
        showFirst = $("#showfirst").prop("checked");
        loadFormer = $("#loadformer").prop("checked");
        console.log(showFirst,loadFormer);
        $.get(`../../data/vocabulary/${$("vocabulary").val()}.json`,(data,status) => {
            if(status>=200&&status<400){
                console.log(data,status);
            }
            else console.log(`Error ${status}`);
        });

        $(".settings").hide();        
    })
}))();


function getVocabularies(){
    let sel = $("#vocabulary");

    for(let i in vocabularies){
        sel.append($("<option></option>").val(i).text(vocabularies[i]));
    }
}