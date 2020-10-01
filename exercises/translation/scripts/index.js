let showFirst,loadFormer,oneTranslation;
let vocabulary;
const vocabularies={
    "mid_vocab": "中考考纲词汇/词组"
};
let current;

($(()=>{
    getVocabularies();

    $("#start").click(()=>{
        showFirst = $("#showfirst").prop("checked");
        loadFormer = $("#loadformer").prop("checked");
        oneTranslation = $("#onetranslation").prop("checked");
        console.log(showFirst,loadFormer);
        $.get(`../../data/vocabulary/${$("#vocabulary").val()}.json`,(data,status) => {
            if(status=="success"){
                vocabulary = data;
                initializeExercise();
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

function initializeExercise(){
    $(".vocabname").text(vocabulary.name);
    generate();
}

function generate(){
    current = vocabulary.vocabulary[Math.floor(Math.random()*vocabulary.vocabulary.length)];
    let meaning = current.meanings[Math.floor(Math.random()*current.meanings.length)];
    let translation = meaning.translations[Math.floor(Math.random()*meaning.translations.length)];
    $(".question").text(`${meaning.type}. ${onetranslation?translation:meaning.translations.join("；")}`);
    if(showFirst) $("#firstletter").text(current.word[0]);
}

function check(){
    let answer = $("trans").val();
    if(showFirst) answer = current.word[0] + answer;
    if(answer==current.word){
        console.log("right");

    }
    else {
        console.log("wrong");
    }
}